using Microsoft.AspNetCore.Mvc;
using VentasApi.DTOs;
using VentasApi.Models;
using VentasApi.Services;

namespace VentasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly JsonDataStore _store;

        public VentasController(JsonDataStore store)
        {
            _store = store;
        }

        /// <summary>
        /// Registrar una venta. Calcula montoTotal desde el precio del libro.
        /// </summary>
        [HttpPost]
    public async Task<IActionResult> RegistrarVenta([FromBody] RegistrarVentaRequest req)
        {
            if (!ModelState.IsValid) return new ContentResult { Content = System.Text.Json.JsonSerializer.Serialize(new { message = "Bad request" }), ContentType = "application/json", StatusCode = StatusCodes.Status400BadRequest };

            // Load related data
            var libros = await _store.GetLibrosAsync();
            var compradores = await _store.GetCompradoresAsync();
            var vendedores = await _store.GetVendedoresAsync();
            var ventas = await _store.GetVentasAsync();

            // Validate unique codigoDeCompra
            if (ventas.Any(v => v.CodigoDeCompra == req.CodigoDeCompra))
            {
                var cJson = System.Text.Json.JsonSerializer.Serialize(new { message = "codigoDeCompra ya existe" });
                return new ContentResult { Content = cJson, ContentType = "application/json", StatusCode = StatusCodes.Status409Conflict };
            }

            var libro = libros.FirstOrDefault(l => l.Id == req.LibroId);
            if (libro == null)
            {
                var j = System.Text.Json.JsonSerializer.Serialize(new { message = "Libro no encontrado" });
                return new ContentResult { Content = j, ContentType = "application/json", StatusCode = StatusCodes.Status404NotFound };
            }

            var comprador = compradores.FirstOrDefault(c => c.Id == req.CompradorId);
            if (comprador == null)
            {
                var j = System.Text.Json.JsonSerializer.Serialize(new { message = "Comprador no encontrado" });
                return new ContentResult { Content = j, ContentType = "application/json", StatusCode = StatusCodes.Status404NotFound };
            }

            var vendedor = vendedores.FirstOrDefault(v => v.Id == req.VendedorId);
            if (vendedor == null)
            {
                var j = System.Text.Json.JsonSerializer.Serialize(new { message = "Vendedor no encontrado" });
                return new ContentResult { Content = j, ContentType = "application/json", StatusCode = StatusCodes.Status404NotFound };
            }

            var fecha = req.FechaDeVenta ?? DateTime.UtcNow;

            var venta = new Venta
            {
                CodigoDeCompra = req.CodigoDeCompra,
                FechaDeVenta = fecha,
                LibroId = req.LibroId,
                CompradorId = req.CompradorId,
                VendedorId = req.VendedorId,
                MontoTotal = Math.Round(libro.Precio, 2),
                IsDeleted = false
            };

            ventas.Add(venta);
            await _store.SaveVentasAsync(ventas);

            var resp = new VentaResponse
            {
                CodigoDeCompra = venta.CodigoDeCompra,
                FechaDeVenta = venta.FechaDeVenta,
                LibroId = venta.LibroId,
                CompradorId = venta.CompradorId,
                VendedorId = venta.VendedorId,
                MontoTotal = venta.MontoTotal,
                IsDeleted = venta.IsDeleted
            };

            // Pre-serialize to avoid issues with TestServer PipeWriter implementations
            var json = System.Text.Json.JsonSerializer.Serialize(resp, new System.Text.Json.JsonSerializerOptions { WriteIndented = false });
            // set Location header to the GET endpoint
            var location = Url.Action(nameof(GetVentaByCodigo), new { codigoDeCompra = venta.CodigoDeCompra });
            if (!string.IsNullOrEmpty(location)) Response.Headers["Location"] = location;
            return new ContentResult { Content = json, ContentType = "application/json", StatusCode = StatusCodes.Status201Created };
        }

        /// <summary>
        /// Obtener venta por codigo (no eliminado)
        /// </summary>
        [HttpGet("{codigoDeCompra}")]
        public async Task<IActionResult> GetVentaByCodigo(int codigoDeCompra)
        {
            var ventas = await _store.GetVentasAsync();
            var venta = ventas.FirstOrDefault(v => v.CodigoDeCompra == codigoDeCompra && !v.IsDeleted);
            if (venta == null) return NotFound();
            var single = new VentaResponse
            {
                CodigoDeCompra = venta.CodigoDeCompra,
                FechaDeVenta = venta.FechaDeVenta,
                LibroId = venta.LibroId,
                CompradorId = venta.CompradorId,
                VendedorId = venta.VendedorId,
                MontoTotal = venta.MontoTotal,
                IsDeleted = venta.IsDeleted
            };
            var jsonSingle = System.Text.Json.JsonSerializer.Serialize(single);
            return new ContentResult { Content = jsonSingle, ContentType = "application/json", StatusCode = StatusCodes.Status200OK };
        }

        /// <summary>
        /// Lista todas las ventas no eliminadas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetVentas()
        {
            var ventas = await _store.GetVentasAsync();
            var result = ventas
                .Where(v => !v.IsDeleted)
                .Select(venta => new VentaResponse
                {
                    CodigoDeCompra = venta.CodigoDeCompra,
                    FechaDeVenta = venta.FechaDeVenta,
                    LibroId = venta.LibroId,
                    CompradorId = venta.CompradorId,
                    VendedorId = venta.VendedorId,
                    MontoTotal = venta.MontoTotal,
                    IsDeleted = venta.IsDeleted
                })
                .ToList();

            var json = System.Text.Json.JsonSerializer.Serialize(result);
            return new ContentResult { Content = json, ContentType = "application/json", StatusCode = StatusCodes.Status200OK };
        }

        /// <summary>
        /// Eliminar (soft-delete) una venta. Como no hay autenticación aún, se requiere pasar vendedorId como query param para comprobar autorización.
        /// </summary>
        [HttpDelete("{codigoDeCompra}")]
        public async Task<IActionResult> EliminarVenta(int codigoDeCompra, [FromQuery] int? vendedorId)
        {
            if (vendedorId == null)
            {
                var j = System.Text.Json.JsonSerializer.Serialize(new { message = "vendedorId es requerido en query string para autorizar la eliminación" });
                return new ContentResult { Content = j, ContentType = "application/json", StatusCode = StatusCodes.Status400BadRequest };
            }

            var vendedores = await _store.GetVendedoresAsync();
            var vendedor = vendedores.FirstOrDefault(v => v.Id == vendedorId.Value);
            if (vendedor == null)
            {
                var j = System.Text.Json.JsonSerializer.Serialize(new { message = "Vendedor no encontrado" });
                return new ContentResult { Content = j, ContentType = "application/json", StatusCode = StatusCodes.Status404NotFound };
            }

            var ventas = await _store.GetVentasAsync();
            var venta = ventas.FirstOrDefault(v => v.CodigoDeCompra == codigoDeCompra);
            if (venta == null)
            {
                var j = System.Text.Json.JsonSerializer.Serialize(new { message = "Venta no encontrada" });
                return new ContentResult { Content = j, ContentType = "application/json", StatusCode = StatusCodes.Status404NotFound };
            }

            if (venta.IsDeleted)
            {
                var j = System.Text.Json.JsonSerializer.Serialize(new { message = "Venta ya eliminada" });
                return new ContentResult { Content = j, ContentType = "application/json", StatusCode = StatusCodes.Status400BadRequest };
            }

            if (venta.VendedorId != vendedorId.Value)
            {
                var j = System.Text.Json.JsonSerializer.Serialize(new { message = "No autorizado" });
                return new ContentResult { Content = j, ContentType = "application/json", StatusCode = StatusCodes.Status403Forbidden };
            }

            // Soft delete
            venta.IsDeleted = true;
            await _store.SaveVentasAsync(ventas);

            return NoContent();
        }
    }
}
