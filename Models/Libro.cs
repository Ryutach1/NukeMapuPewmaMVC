using System.ComponentModel.DataAnnotations;

namespace ProyectoNukeMapuPewmaMVC.Models;

public class Libro
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public required string Nombre { get; set; }

    [Required(ErrorMessage = "El autor es obligatorio")]
    public required string Autor { get; set; }

    public required string Editorial { get; set; }
    
    public required string Categoria { get; set; }
    
    public int Fecha { get; set; }
    
    public required string Descripcion { get; set; } 

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]   
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "La cantidad es obligatoria.")]
    [Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser negativa")]
    public int Cantidad { get; set; }
}
