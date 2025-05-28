using ProyectoNukeMapuPewmaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProyectoNukeMapuPewmaVSC.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILogger<LibroController> _logger;
        private readonly DataContext _context;

        public LibroController(ILogger<LibroController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Libro()//muestra productos en vista artesania
        {
            var libro = await _context.Libro.ToListAsync();  
            return View(libro ?? new List<Libro>());
        }

        public IActionResult LibroCrear()
        {
            return View();
        }

         [HttpPost]
        public async Task<IActionResult> LibroCrear(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Libro));
            }
            return View(libro);
        }
    }
}
