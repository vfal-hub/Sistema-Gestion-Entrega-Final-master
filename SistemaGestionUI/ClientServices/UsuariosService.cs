using Microsoft.AspNetCore.WebUtilities;
using SistemaGestionEntities;
using System.Net.Http.Headers;

namespace SistemaGestionUI.ClientServices;

public class UsuariosService
{
    private readonly HttpClient _httpClient;

    public UsuariosService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Usuario>?> ListUsers()
    {
        return await _httpClient.GetFromJsonAsync<List<Usuario>>("");
    }

    public async Task<Usuario?> GetOneUser(int id)
    {
        return await _httpClient.GetFromJsonAsync<Usuario>($"{id}");
    }


    public async Task<Usuario?> CreateUser(Usuario usuario)
    {
        var response = await _httpClient.PostAsJsonAsync("", usuario);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Usuario>();
        }

        return null;
    }

    public async Task UpdateUser(int id, Usuario usuario)
    {
        await _httpClient.PutAsJsonAsync($"{id}", usuario);
    }

    public async Task DeleteUser(int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }

    public async Task<List<Usuario>?> GetUserBy(string filtro)
    {
        return await _httpClient.GetFromJsonAsync<List<Usuario>>(
            QueryHelpers.AddQueryString("", new Dictionary<string, string>() { { "filtro", filtro } }));
    }

    public async Task<bool> UserExists(string email)
    {
        var usuario = await _httpClient.GetFromJsonAsync<Usuario>(
            QueryHelpers.AddQueryString("api/Usuarios", new Dictionary<string, string>() { { "email", email } }));

        return usuario != null;
    }

    public async Task<SesionDTO?> ValidateUser(string email, string password)
    {
        var loginDTO = new LoginDTO { Email = email, Contraseña = password };

        var response = await _httpClient.PostAsJsonAsync("Login", loginDTO);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<SesionDTO>();
        }

        return null;
    }

    public async Task<string?> GetUserName(LoginDTO login)
    {        
        var response = await _httpClient.PostAsJsonAsync("GetUserName", login);

        if (response.IsSuccessStatusCode)
        {

            var nombreUsuario = await response.Content.ReadAsStringAsync();
            return nombreUsuario;
        }

        return null;
    }

    public async Task<string?> GetUserNameById(int idUsuario)
    {
        var response = await _httpClient.GetAsync($"GetUserNameById/{idUsuario}");

        if (response.IsSuccessStatusCode)
        {
            var nombreUsuario = await response.Content.ReadAsStringAsync();
            return nombreUsuario;
        }

        return null;
    }


}
