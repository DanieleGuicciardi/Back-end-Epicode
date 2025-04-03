using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Services
{
    public class LibroService
    {
        private readonly BibliotecaDbContext _context;

        public LibroService(BibliotecaDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Libro>> GetAllBooksAsync()
        {
            return await _context.Libri.Include(b => b.Prestiti).ToListAsync();
        }

        public async Task<Libro>? GetBookByIdAsync(Guid id)
        {
            return await _context.Libri.Include(l => l.Prestiti).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<bool> AddBookAsync(Libro libro)
        {
            _context.Libri.Add(libro);
            return await SaveAsync();
        }

        public async Task<bool> UpdateBookAsync(Libro libro)
        {
            _context.Entry(libro).State = EntityState.Modified;
            return await SaveAsync();
        }

        public async Task<bool> DeleteBookAsync(Guid id)
        {
            var book = await _context.Libri.FindAsync(id);
            if (book == null)
            {
                return false;
            }

            _context.Libri.Remove(book);
            return await SaveAsync();
        }

        public async Task<List<Libro>> SearchBooksAsync(string parolachiave)
        {
            if (string.IsNullOrEmpty(parolachiave))
            {
                return await GetAllBooksAsync();
            }

            return await _context.Libri
                .Where(b => b.Titolo.Contains(parolachiave) ||
                            b.Autore.Contains(parolachiave) ||
                            b.Genere.Contains(parolachiave))
                .ToListAsync();
        }
    }
}
