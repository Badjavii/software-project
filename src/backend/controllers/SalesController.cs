using Microsoft.AspNetCore.Mvc;
using backend.models;
using backend.services;
using System.Text.Json;
using System.IO;
using System.Linq;

namespace backend.controllers
{
    [ApiController]
    [Route("api/ventas")]
    public class SalesController : ControllerBase
    {
        private readonly SaleService _service = new SaleService();

        [HttpGet]
        public IActionResult Listar()
        {
            var items = _service.ListarVentas();
            return Ok(items);
        }

        public class CreateSaleRequest { public int LibroId { get; set; } public int VendedorId { get; set; } public int CompradorId { get; set; } }

        [HttpPost]
        public IActionResult Registrar([FromBody] CreateSaleRequest req)
        {
            if (req == null) return BadRequest(new { error = "Solicitud inválida" });

            try
            {
                // Verify book exists by _id
                if (!System.IO.File.Exists("data/book.json"))
                    return BadRequest(new { error = "Datos de libros no disponibles" });

                var booksJson = System.IO.File.ReadAllText("data/book.json");
                var books = JsonSerializer.Deserialize<List<JsonElement>>(booksJson) ?? new List<JsonElement>();
                bool bookExists = books.Any(b => b.GetProperty("_id").GetInt32() == req.LibroId);
                if (!bookExists) return BadRequest(new { error = $"Libro con id {req.LibroId} no encontrado" });

                // Verify seller id exists (1-based index into sellers.json)
                if (!System.IO.File.Exists("data/sellers.json"))
                    return BadRequest(new { error = "Datos de vendedores no disponibles" });
                var sellersJson = System.IO.File.ReadAllText("data/sellers.json");
                var sellers = JsonSerializer.Deserialize<List<JsonElement>>(sellersJson) ?? new List<JsonElement>();
                if (req.VendedorId <= 0 || req.VendedorId > sellers.Count)
                    return BadRequest(new { error = $"Vendedor con id {req.VendedorId} no encontrado" });

                // Verify buyer id exists (1-based index into buyers.json)
                if (!System.IO.File.Exists("data/buyers.json"))
                    return BadRequest(new { error = "Datos de compradores no disponibles" });
                var buyersJson = System.IO.File.ReadAllText("data/buyers.json");
                var buyers = JsonSerializer.Deserialize<List<JsonElement>>(buyersJson) ?? new List<JsonElement>();
                if (req.CompradorId <= 0 || req.CompradorId > buyers.Count)
                    return BadRequest(new { error = $"Comprador con id {req.CompradorId} no encontrado" });

                // All simple existence checks passed — register the sale
                var venta = _service.RegistrarVenta(req.LibroId, req.VendedorId, req.CompradorId);
                return Ok(venta);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("{codigo}/soft-delete")]
        public IActionResult SoftDelete([FromRoute] int codigo)
        {
            var ok = _service.SoftDelete(codigo);
            if (!ok) return NotFound(new { error = "Venta no encontrada" });
            return NoContent();
        }

        [HttpDelete("{codigo}/hard")]
        public IActionResult HardDelete([FromRoute] int codigo)
        {
            var ok = _service.HardDelete(codigo);
            if (!ok) return NotFound(new { error = "Venta no encontrada" });
            return NoContent();
        }
    }
}
