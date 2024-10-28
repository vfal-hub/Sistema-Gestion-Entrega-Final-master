using SistemaGestionEntities;
using Microsoft.AspNetCore.WebUtilities;
using SistemaGestionUI.Components.Pages.Productos;

namespace SistemaGestionUI.ClientServices;

public class ProductosVendidosService
{
    private readonly HttpClient _httpClient;

    public ProductosVendidosService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductosVendidos?> ObtenerProductoVendido(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProductosVendidos>($"{id}");
    }

    public async Task<List<ProductosVendidos>?> ListarProductosVendidos()
    {
        return await _httpClient.GetFromJsonAsync<List<ProductosVendidos>>("");
    }

    public async Task CrearProductoVendido(ProductosVendidos productov)
    {
        await _httpClient.PostAsJsonAsync("", productov);
    }

    public async Task ModificarProductoVendido(int id, ProductosVendidos productov)
    {
        await _httpClient.PutAsJsonAsync($"{id}", productov);
    }

    public async Task EliminarProductoVendido(int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }

}
