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
            var artesanias = await _context.Artesanias.ToListAsync();  
            return View(artesanias ?? new List<Artesania>());
        }

        public IActionResult Create()
        {
            return View("~/Views/Home/ArteCrear.cshtml");
        }

         [HttpPost]
        public async Task<IActionResult> Create(Artesania artesania)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artesania);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artesania);
        }
    }
}
