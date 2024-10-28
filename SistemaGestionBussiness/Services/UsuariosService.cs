using Microsoft.EntityFrameworkCore;
using SistemaGestionData.DataAccess;
using SistemaGestionEntities;
using System.Net.Http;

namespace SistemaGestionBussiness.Services;

public class UsuariosService
{
    private UsuariosDataAccess _usuariosDataAccess;

    public UsuariosService(UsuariosDataAccess usuariosDataAccess)
    {
        _usuariosDataAccess = usuariosDataAccess;
    }

    public List<Usuario> ListUsers()
    {
        return _usuariosDataAccess.ListUsers();
    }

    public Usuario? GetOneUser(int id)
    {
        return _usuariosDataAccess.GetOneUser(id);
    }

    public Usuario CreateUser(Usuario usuario)
    {
        return _usuariosDataAccess.CreateUser(usuario);
    }

    public void UpdateUser(int id, Usuario usuario)
    {
        _usuariosDataAccess.UpdateUser(id, usuario);
    }

    public void DeleteUser(int id)
    {
        _usuariosDataAccess.DeleteUser(id);
    }

    public List<Usuario> GetUserBy(string filtro)
    {
        return _usuariosDataAccess.GetUserBy(filtro);
    }

    public bool UserExists(string email)
    {
        return _usuariosDataAccess.UserExists(email);
    }

    public Usuario ValidateUser(string email, string password)
    {
        return _usuariosDataAccess.ValidateUser( email , password);
    }
  
}

