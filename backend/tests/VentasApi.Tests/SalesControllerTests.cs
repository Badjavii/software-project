using System;
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
            LibroId = 1,
            CompradorId = 1,
            VendedorId = 1
        };

        // POST register
        var postResp = await _client.PostAsJsonAsync("/api/ventas", req);
        Assert.Equal(HttpStatusCode.Created, postResp.StatusCode);
        var created = await postResp.Content.ReadFromJsonAsync<VentaResponse>();
        Assert.NotNull(created);
        Assert.Equal(1, created!.CodigoDeCompra);

        // GET list
        var listResp = await _client.GetAsync("/api/ventas");
        Assert.Equal(HttpStatusCode.OK, listResp.StatusCode);
        var ventas = await listResp.Content.ReadFromJsonAsync<VentaResponse[]>();
        Assert.NotNull(ventas);
        Assert.Contains(ventas!, v => v.CodigoDeCompra == created!.CodigoDeCompra);

        // DELETE with wrong vendedorId -> Forbid (403) (vendedor exists but is not owner)
        var delWrong = await _client.DeleteAsync($"/api/ventas/{created!.CodigoDeCompra}?vendedorId=2");
        Assert.Equal(HttpStatusCode.Forbidden, delWrong.StatusCode);

        // DELETE with correct vendedorId -> NoContent
        var delOk = await _client.DeleteAsync($"/api/ventas/{created!.CodigoDeCompra}?vendedorId=1");
        Assert.Equal(HttpStatusCode.NoContent, delOk.StatusCode);

        // GET list should not contain deleted
        var listAfter = await _client.GetAsync("/api/ventas");
        var ventas2 = await listAfter.Content.ReadFromJsonAsync<VentaResponse[]>();
        Assert.DoesNotContain(ventas2!, v => v.CodigoDeCompra == created!.CodigoDeCompra);
    }

    [Fact]
    public async Task Post_Assigns_Sequential_Codigos()
    {
        var req = new RegistrarVentaRequest
        {
            LibroId = 1,
            CompradorId = 1,
            VendedorId = 1
        };

        var first = await _client.PostAsJsonAsync("/api/ventas", req);
        var ventaUno = await first.Content.ReadFromJsonAsync<VentaResponse>();

        var second = await _client.PostAsJsonAsync("/api/ventas", req);
        var ventaDos = await second.Content.ReadFromJsonAsync<VentaResponse>();

        Assert.Equal(HttpStatusCode.Created, first.StatusCode);
        Assert.Equal(HttpStatusCode.Created, second.StatusCode);
        Assert.NotNull(ventaUno);
        Assert.NotNull(ventaDos);
        Assert.Equal(ventaUno!.CodigoDeCompra + 1, ventaDos!.CodigoDeCompra);
    }

    [Fact]
    public async Task Soft_Delete_Endpoint_Marks_Venta()
    {
        var req = new RegistrarVentaRequest
        {
            LibroId = 1,
            CompradorId = 1,
            VendedorId = 1
        };

        var postResp = await _client.PostAsJsonAsync("/api/ventas", req);
        var created = await postResp.Content.ReadFromJsonAsync<VentaResponse>();
        Assert.Equal(HttpStatusCode.Created, postResp.StatusCode);
        Assert.NotNull(created);

        var patchRequest = new HttpRequestMessage(HttpMethod.Patch, $"/api/ventas/{created!.CodigoDeCompra}/soft-delete");
        var patchResp = await _client.SendAsync(patchRequest);
        Assert.Equal(HttpStatusCode.NoContent, patchResp.StatusCode);

        var listResp = await _client.GetAsync("/api/ventas");
        var ventas = await listResp.Content.ReadFromJsonAsync<VentaResponse[]>();
        Assert.NotNull(ventas);
        Assert.DoesNotContain(ventas!, v => v.CodigoDeCompra == created!.CodigoDeCompra);

        var singleResp = await _client.GetAsync($"/api/ventas/{created!.CodigoDeCompra}");
        Assert.Equal(HttpStatusCode.NotFound, singleResp.StatusCode);
    }

    [Fact]
    public async Task Hard_Delete_Removes_Venta()
    {
        var req = new RegistrarVentaRequest
        {
            LibroId = 1,
            CompradorId = 1,
            VendedorId = 1
        };

        var postResp = await _client.PostAsJsonAsync("/api/ventas", req);
        var created = await postResp.Content.ReadFromJsonAsync<VentaResponse>();

        Assert.Equal(HttpStatusCode.Created, postResp.StatusCode);
        Assert.NotNull(created);

        var deleteResp = await _client.DeleteAsync($"/api/ventas/{created!.CodigoDeCompra}/hard");
        Assert.Equal(HttpStatusCode.NoContent, deleteResp.StatusCode);

        var listResp = await _client.GetAsync("/api/ventas");
        var ventas = await listResp.Content.ReadFromJsonAsync<VentaResponse[]>();
        Assert.NotNull(ventas);
        Assert.DoesNotContain(ventas!, v => v.CodigoDeCompra == created!.CodigoDeCompra);
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
