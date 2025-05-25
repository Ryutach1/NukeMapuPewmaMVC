using ProyectoNukeMapuPewmaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProyectoNukeMapuPewmaVSC.Controllers
{
    public class ArtesaniaController : Controller
    {
        private readonly ILogger<ArtesaniaController> _logger;
        private readonly DataContext _context;

        public ArtesaniaController(ILogger<ArtesaniaController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Artesania()//muestra productos en vista artesania
        {
            var artesania = await _context.Artesania.ToListAsync();  
            return View(artesania ?? new List<Artesania>());
        }

        public IActionResult ArteCrear()
        {
            return View();
        }

         [HttpPost]
        public async Task<IActionResult> ArteCrear(Artesania artesania)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artesania);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Artesania));
            }
            return View(artesania);
        }
    }
}
