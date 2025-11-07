using System.ComponentModel.DataAnnotations;

namespace VentasApi.DTOs
{
    public class RegistrarVentaRequest
    {
        [Required]
        public int CodigoDeCompra { get; set; }

        // ISO 8601 optional - if not provided backend will use UTC now
        public DateTime? FechaDeVenta { get; set; }

        [Required]
        public int LibroId { get; set; }

        [Required]
        public int CompradorId { get; set; }

        [Required]
        public int VendedorId { get; set; }
    }
}
