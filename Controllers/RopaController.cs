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

        // Acción para mostrar el formulario de edición
        public async Task<IActionResult> RopaEditar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ropa = await _context.Ropa.FindAsync(id);
            if (ropa == null)
            {
                return NotFound();
            }
            return View(ropa);
        }

        // Acción para procesar la edición
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RopaEditar(int id, Ropa ropa)
        {
            if (id != ropa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ropa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RopaExists(ropa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Ropa));
            }
            return View(ropa);
        }

        // Acción para mostrar la confirmación de eliminación
        public async Task<IActionResult> RopaEliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ropa = await _context.Ropa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ropa == null)
            {
                return NotFound();
            }

            return View(ropa);
        }

        // Acción para procesar la eliminación
        [HttpPost, ActionName("RopaEliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RopaEliminarConfirmado(int id)
        {
            var ropa = await _context.Ropa.FindAsync(id);
            if (ropa != null)
            {
                _context.Ropa.Remove(ropa);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Ropa));
        }

        // Método auxiliar para verificar existencia
        private bool RopaExists(int id)
        {
            return _context.Ropa.Any(e => e.Id == id);
        }
    }
}
