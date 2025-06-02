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

        // Acción para mostrar el formulario de edición
        public async Task<IActionResult> LibroEditar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libro.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        // Acción para procesar la edición
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LibroEditar(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Libro));
            }
            return View(libro);
        }

        // Acción para mostrar la confirmación de eliminación
        public async Task<IActionResult> LibroEliminar(int? id)     //falta vista LibroEliminar
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // Acción para procesar la eliminación
        [HttpPost, ActionName("LibroEliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LibroEliminarConfirmado(int id)
        {
            var libro = await _context.Libro.FindAsync(id);
            if (libro != null)
            {
                _context.Libro.Remove(libro);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Libro));
        }

        // Método auxiliar para verificar existencia
        private bool LibroExists(int id)
        {
            return _context.Libro.Any(e => e.Id == id);
        }
    }
}
