using ProyectoNukeMapuPewmaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;



namespace ProyectoNukeMapuPewmaVSC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private readonly DataContext _context;

        public ProductoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarProductos([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("El término de búsqueda no puede estar vacío");
            }

            var artesanias = await _context.Artesania
                .Where(a => a.Nombre.Contains(query) || a.Descripcion.Contains(query))
                .Select(a => new
                {
                    id = a.Id,
                    nombre = a.Nombre,
                    tipo = "Artesania"
                }).ToListAsync();

            var libros = await _context.Libro
                .Where(l => l.Nombre.Contains(query) || l.Descripcion.Contains(query) || l.Autor.Contains(query))
                .Select(l => new
                {
                    id = l.Id,
                    nombre = l.Nombre,
                    tipo = "Libro"
                }).ToListAsync();

            var ropas = await _context.Ropa
                .Where(r => r.Nombre.Contains(query))
                .Select(r => new
                {
                    id = r.Id,
                    nombre = r.Nombre,
                    tipo = "Ropa"
                }).ToListAsync();

            var otros = await _context.Otros
                .Where(o => o.Descripcion.Contains(query))
                .Select(o => new
                {
                    id = o.Id,
                    nombre = o.Descripcion,
                    tipo = "Otro"
                }).ToListAsync();

            var resultados = artesanias
                .Concat(libros)
                .Concat(ropas)
                .Concat(otros)
                .ToList();

            return Ok(resultados);
        }

        [HttpGet("/Producto/Detalle/{id}")]
        public async Task<IActionResult> Detalle(int id, [FromQuery] string tipo)
        {
            object? producto = tipo.ToLower() switch
            {
                "artesania" => await _context.Artesania.FindAsync(id),
                "libro" => await _context.Libro.FindAsync(id),
                "ropa" => await _context.Ropa.FindAsync(id),
                "otro" => await _context.Otros.FindAsync(id),
                _ => null
            };

            if (producto == null)
            {
                return NotFound();
            }

            return View("DetalleProducto", producto);
        }

    }
}
