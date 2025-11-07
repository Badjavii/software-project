using System.Text.Json.Serialization;

namespace VentasApi.Models
{
    public class Venta
    {
        // Use codigoDeCompra as unique business identifier
        public int CodigoDeCompra { get; set; }

        // ISO 8601 timestamp string (store as DateTime)
        public DateTime FechaDeVenta { get; set; }

        // Calculated from libro.Precio
        public decimal MontoTotal { get; set; }

        // Foreign keys (simple references)
        public int LibroId { get; set; }
        public int CompradorId { get; set; }
        public int VendedorId { get; set; }

        // Soft delete
        public bool IsDeleted { get; set; } = false;
    }
}
