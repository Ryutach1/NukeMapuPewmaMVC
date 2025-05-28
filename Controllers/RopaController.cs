using ProyectoNukeMapuPewmaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProyectoNukeMapuPewmaVSC.Controllers
{
    public class RopaController : Controller
    {
        private readonly ILogger<RopaController> _logger;
        private readonly DataContext _context;

        public RopaController(ILogger<RopaController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Ropa()//muestra productos en vista artesania
        {
            var ropa = await _context.Ropa.ToListAsync();  
            return View(ropa ?? new List<Ropa>());
        }

        public IActionResult RopaCrear()
        {
            return View();
        }

         [HttpPost]
        public async Task<IActionResult> RopaCrear(Ropa ropa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ropa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Ropa));
            }
            return View(ropa);
        }
    }
}
