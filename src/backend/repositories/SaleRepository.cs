using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using backend.models;

namespace backend.repositories
{
    public class SaleRepository
    {
        private static readonly SaleRepository _instance = new SaleRepository();
        private readonly string _jsonPath = @"data/ventas.json";
        private readonly List<Venta> _ventas;

        private SaleRepository()
        {
            _ventas = new List<Venta>();
            Load();
        }

        public static SaleRepository Instance => _instance;

        private void Load()
        {
            if (!File.Exists(_jsonPath)) return;

            var json = File.ReadAllText(_jsonPath);
            try
            {
                var items = JsonSerializer.Deserialize<List<Venta>>(json);
                if (items != null) _ventas.AddRange(items);
            }
            catch
            {
                // ignore malformed file
            }
        }

        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_ventas, options);
            File.WriteAllText(_jsonPath, json);
        }

        public List<Venta> ReturnVentas() => _ventas;

        public Venta? ReturnVentaByCodigo(int codigo)
        {
            return _ventas.FirstOrDefault(v => v.CodigoDeCompra == codigo);
        }

        public void AddVenta(Venta venta)
        {
            _ventas.Add(venta);
        }

        public void RemoveVenta(Venta venta)
        {
            _ventas.Remove(venta);
        }
    }
}