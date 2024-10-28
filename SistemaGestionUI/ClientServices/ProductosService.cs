using SistemaGestionEntities;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http;
using System.Threading.Tasks;
using SistemaGestionUI.Components.Pages.Productos;

namespace SistemaGestionUI.ClientServices;

public class ProductosService
{
    private readonly HttpClient _httpClient;

    public ProductosService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Producto>?> ListProducts()
    {
        return await _httpClient.GetFromJsonAsync<List<Producto>>("");
    }

    public async Task<List<Producto>?> GetProductBy(string filtro)
    {
        return await _httpClient.GetFromJsonAsync<List<Producto>>(
            QueryHelpers.AddQueryString("", new Dictionary<string, string>() { { "filtro", filtro } }));
    }

    public async Task<Producto?> GetOneProduct(int id)
    {
        return await _httpClient.GetFromJsonAsync<Producto>($"{id}");
    }

    public async Task InsertProduct(Producto producto)
    {
        await _httpClient.PostAsJsonAsync("", producto);
    }

    public async Task UpdateProduct(int id, Producto producto)
    {
        await _httpClient.PutAsJsonAsync($"{id}", producto);
    }

  
    public async Task DeleteProduct(int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }

    public Task UpdateTotalProducts()
    {
        return _httpClient.PutAsync("fix-total", null);
    }

}
