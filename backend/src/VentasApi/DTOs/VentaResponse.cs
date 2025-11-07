namespace VentasApi.DTOs
{
    public class VentaResponse
    {
        public int CodigoDeCompra { get; set; }
        public DateTime FechaDeVenta { get; set; }
        public decimal MontoTotal { get; set; }
        public int LibroId { get; set; }
        public int CompradorId { get; set; }
        public int VendedorId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
