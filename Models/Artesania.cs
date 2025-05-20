namespace ProyectoNukeMapuPewmaMVC.Models;

public class Artesania
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
}
