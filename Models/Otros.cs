namespace ProyectoNukeMapuPewmaMVC.Models
{
    public class Otros
    {
        public int Id { get; set; }
        public required string Tipo { get; set; }
        public required string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}