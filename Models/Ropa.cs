
using System.ComponentModel.DataAnnotations;
namespace ProyectoNukeMapuPewmaMVC.Models;

public class Ropa
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public required string Nombre { get; set; }

    public required string Talla { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "La cantidad es obligatoria.")]
    [Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser negativa")]
    public int Cantidad { get; set; }

    public string? Estado { get; set; }
    public string? ImagenRuta { get; set; }
}
