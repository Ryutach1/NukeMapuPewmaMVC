namespace ProyectoNukeMapuPewmaMVC.Models;

public class Libro
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public required string Autor { get; set; }
    public required string Editorial { get; set; }
    public required string Categoria { get; set; }
    public int Fecha { get; set; }
    public required string Descripcion { get; set; }    
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
}
