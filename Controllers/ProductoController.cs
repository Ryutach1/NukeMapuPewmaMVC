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

            var productos = await _context.Artesania
                .Where(a => a.Nombre.Contains(query) || a.Descripcion.Contains(query))
                .Take(50)
                .ToListAsync();

            return Ok(productos);
        }

        [HttpGet("detalles/{id}")]
        public async Task<IActionResult> ObtenerDetalles(int id)
        {
            var producto = await _context.Artesania.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }
    }
}
