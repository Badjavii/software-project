using System;
namespace backend.models
{
    public class Venta
    {
        public int CodigoDeCompra {get; set;}
        public DateTime FechaDeVenta {get; set;}
        public double MontoTotal {get; set;}
        public int LibroId {get; set;}
        public int CompradorId {get; set;}
        public int VendedorId {get; set;}
        public bool IsDeleted {get; set;}
    }
}