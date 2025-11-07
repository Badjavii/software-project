namespace VentasApi.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        // Price stored as decimal with 2 decimal places
        public decimal Precio { get; set; }
    }
}
