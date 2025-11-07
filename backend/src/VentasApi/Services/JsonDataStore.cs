using System.Text.Json;
using VentasApi.Models;

namespace VentasApi.Services
{
    /// <summary>
    /// Simple JSON-backed data store. Not optimized for concurrency; suitable for mock/sprint.
    /// Files are stored under data/ relative to the application root.
    /// </summary>
    public class JsonDataStore
    {
        private readonly string _dataDir;
        private readonly JsonSerializerOptions _opts = new() { PropertyNameCaseInsensitive = true, WriteIndented = true };

        public JsonDataStore(IHostEnvironment env)
        {
            _dataDir = Path.Combine(env.ContentRootPath, "data");
            Directory.CreateDirectory(_dataDir);
        }

        // Generic read/write helpers
        private string PathFor(string fileName) => Path.Combine(_dataDir, fileName);

        public Task<List<T>> ReadListAsync<T>(string fileName)
        {
            var path = PathFor(fileName);
            if (!File.Exists(path)) return Task.FromResult(new List<T>());
            var txt = File.ReadAllText(path);
            if (string.IsNullOrWhiteSpace(txt)) return Task.FromResult(new List<T>());
            var list = JsonSerializer.Deserialize<List<T>>(txt, _opts) ?? new List<T>();
            return Task.FromResult(list);
        }

        public Task WriteListAsync<T>(string fileName, List<T> items)
        {
            var path = PathFor(fileName);
            var txt = JsonSerializer.Serialize(items, _opts);
            File.WriteAllText(path, txt);
            return Task.CompletedTask;
        }

        // Domain-specific helpers
        public Task<List<Libro>> GetLibrosAsync() => ReadListAsync<Libro>("libros.json");
        public Task<List<Persona>> GetCompradoresAsync() => ReadListAsync<Persona>("compradores.json");
        public Task<List<Persona>> GetVendedoresAsync() => ReadListAsync<Persona>("vendedores.json");
        public Task<List<Venta>> GetVentasAsync() => ReadListAsync<Venta>("ventas.json");

        public Task SaveLibrosAsync(List<Libro> items) => WriteListAsync("libros.json", items);
        public Task SaveCompradoresAsync(List<Persona> items) => WriteListAsync("compradores.json", items);
        public Task SaveVendedoresAsync(List<Persona> items) => WriteListAsync("vendedores.json", items);
        public Task SaveVentasAsync(List<Venta> items) => WriteListAsync("ventas.json", items);
    }
}
