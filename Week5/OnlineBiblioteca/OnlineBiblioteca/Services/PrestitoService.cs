using Biblioteca.Data;
using Biblioteca.Models;
using FluentEmail.Core;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Services
{
    public class PrestitoService
    {
        private readonly BibliotecaDbContext _context;
        private readonly IFluentEmail _fluentEmail;
        private readonly LibroService _libroService;

        public PrestitoService(
            BibliotecaDbContext context,
            IFluentEmail fluentEmail,
            LibroService libroService)
        {
            _context = context;
            _fluentEmail = fluentEmail;
            _libroService = libroService;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Prestito>> GetOnGoingLoansAsync()
        {

            List<Prestito>? prestitiTotali = await _context.Prestiti
                .Where(p => p.DataRestituzioneEffettiva == null)
                .Include(l => l.Libro)
                .ToListAsync();

            foreach (var prestito in prestitiTotali)
            {
                prestito.Scaduto = prestito.DataRestituzione < DateTime.Now &&
                                  prestito.DataRestituzioneEffettiva == null;
            }

            return prestitiTotali;
        }

        public async Task<List<Prestito>> GetReturnedLoansAsync()
        {

            List<Prestito>? prestitiTotali = await _context.Prestiti
                .Where(p => p.DataRestituzioneEffettiva != null)
                .Include(l => l.Libro)
                .ToListAsync();

            foreach (var prestito in prestitiTotali)
            {
                prestito.Scaduto = prestito.DataRestituzione < DateTime.Now &&
                                  prestito.DataRestituzioneEffettiva == null;
            }

            return prestitiTotali;
        }

        public async Task<Prestito>? GetLoanByIdAsync(Guid id)
        {
            return await _context.Prestiti
                .Include(l => l.Libro)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<bool> AddLoanAsync(Prestito prestito)
        {
            // Verifica disponibilità del libro
            var libro = await _libroService.GetBookByIdAsync(prestito.LibroId);
            if (libro == null || !libro.Disponibile)
            {
                throw new InvalidOperationException("Il libro non è disponibile per il prestito");
            }

            // Aggiorna stato del libro
            libro.Disponibile = false;
            await _libroService.UpdateBookAsync(libro);

            // Salva prestito
            _context.Prestiti.Add(prestito);

            var aggiuntoConSuccesso = await SaveAsync();

            if (!aggiuntoConSuccesso)
            {
                return aggiuntoConSuccesso;
            }

            // Invia email di conferma
            var response = await _fluentEmail
                .To(prestito.EmailUtente)
                .Subject($"Prestito Libro: {libro.Titolo}")
                .Body($"Gentile {prestito.NomeUtente},\n\nLe confermiamo il prestito del libro '{libro.Titolo}' di {libro.Autore}.\nLa data di restituzione è fissata per il {prestito.DataRestituzione:dd/MM/yyyy}.\n\nGrazie per utilizzare i nostri servizi.\n\nBiblioteca App")
                .SendAsync();

            return aggiuntoConSuccesso;
        }

        public async Task<bool> UpdateLoanAsync(Prestito prestito)
        {
            _context.Entry(prestito).State = EntityState.Modified;
            return await SaveAsync();
        }

        public async Task<bool> ReturnBookAsync(Guid prestitoId)
        {
            var prestito = await GetLoanByIdAsync(prestitoId);

            if (prestito == null)
            {
                throw new InvalidOperationException("Prestito non trovato");
            }

            // Aggiorna prestito
            prestito.DataRestituzioneEffettiva = DateTime.Now;

            // Aggiorna disponibilità libro
            var libro = await _libroService.GetBookByIdAsync(prestito.LibroId);

            if(libro == null)
            {
                throw new InvalidOperationException("libro non trovato");
            }

            libro.Disponibile = true;

            // Salva modifiche
            await _libroService.UpdateBookAsync(libro);
            await UpdateLoanAsync(prestito);

            // Invia email di conferma restituzione
            await _fluentEmail
                .To(prestito.EmailUtente)
                .Subject($"Restituzione Libro: {libro.Titolo}")
                .Body($"Gentile {prestito.NomeUtente},\n\nLe confermiamo l'avvenuta restituzione del libro '{libro.Titolo}' in data {prestito.DataRestituzioneEffettiva:dd/MM/yyyy}.\n\nGrazie per utilizzare i nostri servizi.\n\nBiblioteca App")
                .SendAsync();

            return true;
        }

        public async Task SendReminderEmailAsync(Guid prestitoId)
        {
            var prestito = await GetLoanByIdAsync(prestitoId);
            if (prestito == null)
            {
                throw new InvalidOperationException("Prestito non trovato");
            }

            await _fluentEmail
                .To(prestito.EmailUtente)
                .Subject($"Promemoria restituzione: {prestito.Libro.Titolo}")
                .Body($"Gentile {prestito.NomeUtente},\n\nLe ricordiamo che il prestito del libro '{prestito.Libro.Titolo}' scadrà il {prestito.DataRestituzione:dd/MM/yyyy}.\nLa preghiamo di procedere alla restituzione entro tale data.\n\nGrazie per la collaborazione.\n\nBiblioteca App")
                .SendAsync();
        }

        public async Task<List<Prestito>> GetOverdueLoansAsync()
        {
            var today = DateTime.Today;
            return await _context.Prestiti
                .Include(l => l.Libro)
                .Where(l => l.DataRestituzione < today && l.DataRestituzioneEffettiva == null)
                .ToListAsync();
        }

        public async Task<List<Prestito>> GetLoansByBookIdAsync(Guid libroId)
        {
            return await _context.Prestiti
                .Include(l => l.Libro)
                .Where(l => l.LibroId == libroId)
                .ToListAsync();
        }
    }
}
