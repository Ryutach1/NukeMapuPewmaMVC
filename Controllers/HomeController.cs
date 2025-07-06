using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoNukeMapuPewmaMVC.Models;

namespace ProyectoNukeMapuPewmaMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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
