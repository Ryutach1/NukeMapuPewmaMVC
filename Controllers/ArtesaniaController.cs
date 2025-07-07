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
        public async Task<IActionResult> ArteCrear(Artesania artesania, IFormFile? Imagen)
        {
            if (ModelState.IsValid)
            {
                if (Imagen != null && Imagen.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productosImg");
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Imagen.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Imagen.CopyToAsync(stream);
                    }

                    artesania.ImagenRuta = "/productosImg/" + fileName;
                }
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
        public async Task<IActionResult> ArteEditar(int id, Artesania artesania, IFormFile? Imagen)
        {
            if (id != artesania.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(artesania);

            try
            {
                var artesaniaExistente = await _context.Artesania.FirstOrDefaultAsync(a => a.Id == id);
                if (artesaniaExistente == null)
                    return NotFound();

                // Mantener ruta imagen anterior
                string? imagenAnterior = artesaniaExistente.ImagenRuta;

                // Asignar datos nuevos
                artesaniaExistente.Nombre = artesania.Nombre;
                artesaniaExistente.Descripcion = artesania.Descripcion;
                artesaniaExistente.Precio = artesania.Precio;
                artesaniaExistente.Cantidad = artesania.Cantidad;

                if (Imagen != null && Imagen.Length > 0)
                {
                    if (!string.IsNullOrEmpty(imagenAnterior))
                    {
                        var rutaAnterior = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagenAnterior.TrimStart('/'));
                        if (System.IO.File.Exists(rutaAnterior))
                            System.IO.File.Delete(rutaAnterior);
                    }

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productosImg");
                    Directory.CreateDirectory(uploadsFolder); // asegura carpeta
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Imagen.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Imagen.CopyToAsync(stream);
                    }

                    artesaniaExistente.ImagenRuta = "/productosImg/" + fileName;
                }

                _context.Artesania.Update(artesaniaExistente); // ← fuerza actualización
                await _context.SaveChangesAsync();

                TempData["ArtesaniaMensaje"] = "Artesanía actualizada correctamente.";
                return RedirectToAction("Artesania");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando artesanía");
                return View(artesania);
            }
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
                if (!string.IsNullOrEmpty(artesania.ImagenRuta))
                {
                    var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", artesania.ImagenRuta.TrimStart('/'));
                    if (System.IO.File.Exists(ruta))
                    {
                        System.IO.File.Delete(ruta);
                    }
                }
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
