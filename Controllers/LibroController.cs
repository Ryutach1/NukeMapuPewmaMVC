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
        public async Task<IActionResult> LibroCrear(Libro libro, IFormFile? Imagen)
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

                    libro.ImagenRuta = "/productosImg/" + fileName;
                }
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
        public async Task<IActionResult> LibroEditar(int id, Libro libro, IFormFile? Imagen)
        {
            if (id != libro.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(libro);

            try
            {
                var libroExistente = await _context.Libro.FirstOrDefaultAsync(l => l.Id == id);
                if (libroExistente == null)
                    return NotFound();

                // Guardar la ruta anterior (si existe)
                string? imagenAnterior = libroExistente.ImagenRuta;

                // Actualizar campos del libro
                libroExistente.Nombre = libro.Nombre;
                libroExistente.Autor = libro.Autor;
                libroExistente.Editorial = libro.Editorial;
                libroExistente.Categoria = libro.Categoria;
                libroExistente.Fecha = libro.Fecha;
                libroExistente.Descripcion = libro.Descripcion;
                libroExistente.Precio = libro.Precio;
                libroExistente.Cantidad = libro.Cantidad;

                // Si se subió una nueva imagen
                if (Imagen != null && Imagen.Length > 0)
                {
                    // Eliminar imagen anterior si existe
                    if (!string.IsNullOrEmpty(imagenAnterior))
                    {
                        var rutaAnterior = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagenAnterior.TrimStart('/'));
                        if (System.IO.File.Exists(rutaAnterior))
                            System.IO.File.Delete(rutaAnterior);
                    }

                    // Guardar nueva imagen
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productosImg");
                    Directory.CreateDirectory(uploadsFolder); // asegúrate de que la carpeta exista
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Imagen.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Imagen.CopyToAsync(stream);
                    }

                    libroExistente.ImagenRuta = "/productosImg/" + fileName;
                }

                _context.Libro.Update(libroExistente);
                await _context.SaveChangesAsync();

                TempData["LibroMensaje"] = "Libro actualizado correctamente.";
                return RedirectToAction("Libro");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando libro");
                return View(libro);
            }
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
                if (!string.IsNullOrEmpty(libro.ImagenRuta))
                {
                    var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", libro.ImagenRuta.TrimStart('/'));
                    if (System.IO.File.Exists(ruta))
                    {
                        System.IO.File.Delete(ruta);
                    }
                }
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
