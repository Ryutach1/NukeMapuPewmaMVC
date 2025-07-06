using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoNukeMapuPewmaMVC.Models;

namespace ProyectoNukeMapuPewmaMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DataContext _context;

    public HomeController(ILogger<HomeController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var notificaciones = new List<dynamic>();

        var artesanias = await _context.Artesania
            .Where(a => a.Cantidad < 2)
            .Select(a => new { Tipo = "Artesanía", a.Nombre, a.Cantidad })
            .ToListAsync();

        var libros = await _context.Libro
            .Where(l => l.Cantidad < 2)
            .Select(l => new { Tipo = "Libro", l.Nombre, l.Cantidad })
            .ToListAsync();

        var ropas = await _context.Ropa
            .Where(r => r.Cantidad < 2)
            .Select(r => new { Tipo = "Ropa", r.Nombre, r.Cantidad })
            .ToListAsync();

        // Los añadimos todos a la lista general
        notificaciones.AddRange(artesanias);
        notificaciones.AddRange(libros);
        notificaciones.AddRange(ropas);

        return View(notificaciones); // <- Devuelve lista dinámica
    }
    public IActionResult Artesania()
    {
        return View();
    }
    public IActionResult ArteCrear()
    {
        return View();
    }
    public IActionResult Libros()
    {
        return View();
    }
    public IActionResult Ropa()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Catalogo()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
