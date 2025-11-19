using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using backend.models;
using backend.repositories;

namespace backend.services
{
    public class SaleService
    {
        private readonly SaleRepository _saleRepository = SaleRepository.Instance;
        private readonly SellerRepository _sellerRepository = SellerRepository.Instance;
        private readonly BuyerRepository _buyerRepository = BuyerRepository.Instance;

        public List<Venta> ListarVentas()
        {
            return _saleRepository.ReturnVentas().Where(v => !v.IsDeleted).ToList();
        }

        public Venta RegistrarVenta(int libroId, int vendedorId, int compradorId)
        {
            var ventas = _saleRepository.ReturnVentas();
            int nextCodigo = ventas.Any() ? ventas.Max(v => v.CodigoDeCompra) + 1 : 1;

            double monto = 0;
            try
            {
                if (File.Exists("data/book.json"))
                {
                    var booksJson = File.ReadAllText("data/book.json");
                    var books = JsonSerializer.Deserialize<List<JsonElement>>(booksJson) ?? new List<JsonElement>();
                    var book = books.FirstOrDefault(b => b.GetProperty("_id").GetInt32() == libroId);
                    if (book.ValueKind != JsonValueKind.Undefined && book.TryGetProperty("_cost", out var costProp))
                    {
                        // _cost may be number (float/double)
                        if (costProp.ValueKind == JsonValueKind.Number && costProp.TryGetDouble(out var d))
                            monto = d;
                    }
                }
            }
            catch
            {
                // ignore parsing errors and keep monto = 0
            }

            var venta = new Venta
            {
                CodigoDeCompra = nextCodigo,
                FechaDeVenta = DateTime.Now,
                MontoTotal = monto,
                LibroId = libroId,
                CompradorId = compradorId,
                VendedorId = vendedorId,
                IsDeleted = false
            };

            _saleRepository.AddVenta(venta);
            _saleRepository.Save();
            return venta;
        }

        public bool SoftDelete(int codigo)
        {
            var venta = _saleRepository.ReturnVentaByCodigo(codigo);
            if (venta == null) return false;
            if (venta.IsDeleted) return true;
            venta.IsDeleted = true;
            _saleRepository.Save();
            return true;
        }

        public bool HardDelete(int codigo)
        {
            var venta = _saleRepository.ReturnVentaByCodigo(codigo);
            if (venta == null) return false;
            _saleRepository.RemoveVenta(venta);
            _saleRepository.Save();
            return true;
        }
    }
}
