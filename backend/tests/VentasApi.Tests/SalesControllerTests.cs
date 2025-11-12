dtousing System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using VentasApi.DTOs;
using System.Text.Json;

namespace VentasApi.Tests;

public class SalesControllerTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly string _tempDir;
    private readonly HttpClient _client;

    public SalesControllerTests(WebApplicationFactory<Program> factory)
    {
        // Create a temporary content root with data files
        _tempDir = Path.Combine(Path.GetTempPath(), "ventas_tests_" + Guid.NewGuid().ToString("n"));
        Directory.CreateDirectory(_tempDir);

        // Create data directory and write sample data files
        var dataDir = Path.Combine(_tempDir, "data");
        Directory.CreateDirectory(dataDir);

        File.WriteAllText(Path.Combine(dataDir, "libros.json"), JsonSerializer.Serialize(new[] {
            new { Id = 1, Titulo = "A", Autor = "X", Precio = 10.5m }
        }, new JsonSerializerOptions { WriteIndented = true }));

        File.WriteAllText(Path.Combine(dataDir, "compradores.json"), JsonSerializer.Serialize(new[] {
            new { Id = 1, Nombre = "Comprador Uno", Email = "c@e.com" }
        }, new JsonSerializerOptions { WriteIndented = true }));

        File.WriteAllText(Path.Combine(dataDir, "vendedores.json"), JsonSerializer.Serialize(new[] {
            new { Id = 1, Nombre = "Vendedor Uno", Email = "v@e.com" },
            new { Id = 2, Nombre = "Vendedor Dos", Email = "v2@e.com" }
        }, new JsonSerializerOptions { WriteIndented = true }));

        File.WriteAllText(Path.Combine(dataDir, "ventas.json"), JsonSerializer.Serialize(new object[] { }));

        // Configure factory to use content root = tempDir
        _factory = factory.WithWebHostBuilder(builder => {
            builder.UseSetting("contentRoot", _tempDir);
        });

        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Post_Get_Delete_Flow()
    {
        var req = new RegistrarVentaRequest
        {
            CodigoDeCompra = 5001,
            LibroId = 1,
            CompradorId = 1,
            VendedorId = 1
        };

        // POST register
        var postResp = await _client.PostAsJsonAsync("/api/ventas", req);
        Assert.Equal(HttpStatusCode.Created, postResp.StatusCode);

        // GET list
        var listResp = await _client.GetAsync("/api/ventas");
        Assert.Equal(HttpStatusCode.OK, listResp.StatusCode);
        var ventas = await listResp.Content.ReadFromJsonAsync<VentaResponse[]>();
        Assert.NotNull(ventas);
        Assert.Contains(ventas!, v => v.CodigoDeCompra == 5001);

    // DELETE with wrong vendedorId -> Forbid (403) (vendedor exists but is not owner)
    var delWrong = await _client.DeleteAsync($"/api/ventas/5001?vendedorId=2");
    Assert.Equal(HttpStatusCode.Forbidden, delWrong.StatusCode);

        // DELETE with correct vendedorId -> NoContent
        var delOk = await _client.DeleteAsync($"/api/ventas/5001?vendedorId=1");
        Assert.Equal(HttpStatusCode.NoContent, delOk.StatusCode);

        // GET list should not contain deleted
        var listAfter = await _client.GetAsync("/api/ventas");
        var ventas2 = await listAfter.Content.ReadFromJsonAsync<VentaResponse[]>();
        Assert.DoesNotContain(ventas2!, v => v.CodigoDeCompra == 5001);
    }

    [Fact]
    public async Task Post_Duplicate_Code_Returns_Conflict()
    {
        var req = new RegistrarVentaRequest
        {
            CodigoDeCompra = 6001,
            LibroId = 1,
            CompradorId = 1,
            VendedorId = 1
        };

        var first = await _client.PostAsJsonAsync("/api/ventas", req);
        Assert.Equal(HttpStatusCode.Created, first.StatusCode);

        var second = await _client.PostAsJsonAsync("/api/ventas", req);
        Assert.Equal(HttpStatusCode.Conflict, second.StatusCode);
    }

    public void Dispose()
    {
        try
        {
            if (Directory.Exists(_tempDir)) Directory.Delete(_tempDir, true);
        }
        catch { }
    }
}
