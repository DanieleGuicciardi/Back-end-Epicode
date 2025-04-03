using Biblioteca.Models;
using Biblioteca.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Biblioteca.Controllers
{
    public class PrestitiController : Controller
    {
        private readonly PrestitoService _prestitoService;
        private readonly LibroService _libroService;

        public PrestitiController(
            PrestitoService prestitoService,
            LibroService libroService)
        {
            _prestitoService = prestitoService;
            _libroService = libroService;
        }
        public async Task<IActionResult> Index()
        {
            var prestiti = await _prestitoService.GetOnGoingLoansAsync();
            return View(prestiti);
        }
        public async Task<IActionResult> Returned()
        {
            var prestiti = await _prestitoService.GetReturnedLoansAsync();
            return View(prestiti);
        }


        public async Task<IActionResult> Overdue()
        {
            var prestiti = await _prestitoService.GetOverdueLoansAsync();
            return View(prestiti);
        }

        [HttpGet("/prestito/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var prestito = await _prestitoService.GetLoanByIdAsync(id);
            if (prestito == null)
            {
                return NotFound();
            }

            return View(prestito);
        }

        public async Task<IActionResult> Create()
        {
            var libri = await _libroService.GetAllBooksAsync();
            ViewBag.BookList = new SelectList(libri.Where(b => b.Disponibile), "Id", "Titolo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prestito prestito)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _prestitoService.AddLoanAsync(prestito);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var libri = await _libroService.GetAllBooksAsync();
            ViewBag.BookList = new SelectList(libri.Where(b => b.Disponibile), "Id", "Titolo", prestito.LibroId);
            return View(prestito);
        }

        [HttpGet("/edit/by-id/{id:guid}", Name = "Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var prestito = await _prestitoService.GetLoanByIdAsync(id);
            if (prestito == null)
            {
                return NotFound();
            }

            var libri = await _libroService.GetAllBooksAsync();
            ViewBag.BookList = new SelectList(libri.Where(b => b.Disponibile), "Id", "Titolo");
            
            return View(prestito);
        }

        [HttpPost("/edit/by-id/{id:guid}/save", Name = "EditSave")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Prestito prestito)
        {
            if (id != prestito.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var vecchioLibro = await _libroService.GetBookByIdAsync((Guid)TempData["OldId"]);

                    vecchioLibro.Disponibile = true;

                    await _libroService.UpdateBookAsync(vecchioLibro);



                    var prestitoDaAggiornare = await _prestitoService.GetLoanByIdAsync(id);


                    prestitoDaAggiornare.NomeUtente = prestito.NomeUtente;
                    prestitoDaAggiornare.EmailUtente = prestito.EmailUtente;
                    prestitoDaAggiornare.LibroId = prestito.LibroId;

                    var result = await _prestitoService.SaveAsync();

                    if (result)
                    {
                        var nuovoLibroNonDisponibile = await _libroService.GetBookByIdAsync(prestitoDaAggiornare.LibroId);
                        nuovoLibroNonDisponibile.Disponibile = false;
                        await _libroService.SaveAsync();
                    }

                    //await _prestitoService.UpdateLoanAsync(prestito);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    if (await _prestitoService.GetLoanByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    throw;
                }
            }

            var libri = await _libroService.GetAllBooksAsync();
            ViewBag.BookList = new SelectList(libri, "Id", "Titolo", prestito.LibroId);
            return View(prestito);
        }

        [HttpPost("/prestito/return/{id:guid}")]
        public async Task<IActionResult> Return(Guid id)
        {
            var prestito = await _prestitoService.GetLoanByIdAsync(id);
            if (prestito == null)
            {
                return NotFound();
            }

            return View(prestito);
        }

        [HttpGet("/prestito/return/{id:guid}/confirmed")]
        public async Task<IActionResult> ReturnConfirmed(Guid id)
        {
            try
            {
                await _prestitoService.ReturnBookAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var prestito = await _prestitoService.GetLoanByIdAsync(id);
                return View(prestito);
            }
        }

        [HttpGet("/prestito/reminder/{id:guid}")]
        public async Task<IActionResult> SendReminder(Guid id)
        {
            try
            {
                await _prestitoService.SendReminderEmailAsync(id);
                TempData["Message"] = "Promemoria inviato con successo";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Errore nell'invio del promemoria: " + ex.Message;
            }

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
