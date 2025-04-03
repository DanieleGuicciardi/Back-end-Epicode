using Biblioteca.Models;
using Biblioteca.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Biblioteca.Controllers
{
    public class LibriController : Controller
    {
        private readonly LibroService _libroService;

        public LibriController(LibroService libroService)
        {
            _libroService = libroService;
        }

        [HttpGet("/libro/search")]
        public async Task<IActionResult> Index([FromQuery] string parolachiave)
        {

            if (string.IsNullOrEmpty(parolachiave))
            {
                return RedirectToAction("Index", "Home");
            }

            var libri = await _libroService.SearchBooksAsync(parolachiave);
            ViewData["CurrentFilter"] = parolachiave;
            return View(libri);
        }

        [HttpGet("/libro/by-id/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var libro = await _libroService.GetBookByIdAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        [Route("/libro/aggiungi", Name = "AggiungiLibro")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("/libro/aggiungi/salva")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Home", "Index");
            }
            
            // Gestione del caricamento dell'immagine
            if (libro.ImmagineCopertina != null)
            {
                string uploadsFolder = Path.Combine(Environment.CurrentDirectory, "wwwroot", "images", "covers");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + libro.ImmagineCopertina.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await libro.ImmagineCopertina.CopyToAsync(fileStream);
                }

                libro.PercorsoImmagineCopertina = "/images/covers/" + uniqueFileName;
            }

            var libroAggiunto = await _libroService.AddBookAsync(libro);

            if (!libroAggiunto)
            {
                throw new InvalidOperationException("libro non aggiunto");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("libro/edit/by-id/{id:guid}", Name = "EditPage")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var libro = await _libroService.GetBookByIdAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        [HttpPost("libro/edit/by-id/{id:guid}/save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Libro libro)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Gestione dell'aggiornamento della copertina
                    if (libro.ImmagineCopertina != null)
                    {
                        string uploadsFolder = Path.Combine(Environment.CurrentDirectory, "images", "covers");
                        Directory.CreateDirectory(uploadsFolder);

                        // Elimina l'immagine precedente se esiste
                        if (!string.IsNullOrEmpty(libro.PercorsoImmagineCopertina))
                        {
                            string oldImagePath = Path.Combine(Environment.CurrentDirectory, libro.PercorsoImmagineCopertina.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + libro.ImmagineCopertina.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await libro.ImmagineCopertina.CopyToAsync(fileStream);
                        }

                        libro.PercorsoImmagineCopertina = "/images/covers/" + uniqueFileName;
                    }

                    await _libroService.UpdateBookAsync(libro);
                }
                catch (Exception)
                {
                    if (await _libroService.GetBookByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction("Index", "Home");
            }

            return View(libro);
        }

        [HttpGet("libro/delete/by-id/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var libro = await _libroService.GetBookByIdAsync(id);

            // Elimina l'immagine di copertina se esiste
            if (!string.IsNullOrEmpty(libro.PercorsoImmagineCopertina))
            {
                string imagePath = Path.Combine(Environment.CurrentDirectory, libro.PercorsoImmagineCopertina.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            await _libroService.DeleteBookAsync(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
