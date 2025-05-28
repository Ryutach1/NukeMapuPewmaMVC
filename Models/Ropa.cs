namespace ProyectoNukeMapuPewmaMVC.Models;

public class Ropa
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public required string Talla { get; set; }
    public required string Descripcion { get; set; }    
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
}
