using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
using Biblioteca.Services;

namespace Biblioteca.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly LibroService _libroService;
    private readonly PrestitoService _prestitoService;

    public HomeController(
        ILogger<HomeController> logger,
        LibroService libroService,
        PrestitoService prestitoService)
    {
        _logger = logger;
        _libroService = libroService;
        _prestitoService = prestitoService;
    }

    [HttpGet("/")]
    public async Task<IActionResult> Index()
    {
        var books = await _libroService.GetAllBooksAsync();

        ViewBag.TotalBooks = books.Count;
        ViewBag.AvailableBooks = books.Count(b => b.Disponibile);

        return View(books.ToList());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
