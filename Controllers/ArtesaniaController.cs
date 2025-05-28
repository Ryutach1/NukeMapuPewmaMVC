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
        
        // Acción para mostrar el formulario de edición
        public async Task<IActionResult> ArteEditar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artesania = await _context.Artesania.FindAsync(id);
            if (artesania == null)
            {
                return NotFound();
            }
            return View(artesania);
        }

        // Acción para procesar la edición
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArteEditar(int id, Artesania artesania)
        {
            if (id != artesania.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artesania);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtesaniaExists(artesania.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Artesania));
            }
            return View(artesania);
        }

        // Acción para mostrar la confirmación de eliminación
        public async Task<IActionResult> ArteEliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artesania = await _context.Artesania
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artesania == null)
            {
                return NotFound();
            }

            return View(artesania);
        }

        // Acción para procesar la eliminación
        [HttpPost, ActionName("ArteEliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArteEliminarConfirmado(int id)
        {
            var artesania = await _context.Artesania.FindAsync(id);
            if (artesania != null)
            {
                _context.Artesania.Remove(artesania);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Artesania));
        }

        // Método auxiliar para verificar existencia
        private bool ArtesaniaExists(int id)
        {
            return _context.Artesania.Any(e => e.Id == id);
        }
    }
}
