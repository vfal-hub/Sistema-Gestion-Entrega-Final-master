using SistemaGestionData.Context;
using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionData.DataAccess;
public class UsuariosDataAccess
{
    private CoderhouseContext _context = new CoderhouseContext();

    public UsuariosDataAccess()
    {
        _context = new CoderhouseContext();
    }

    public Usuario? GetOneUser(int id)
    {
        return _context.Usuarios.FirstOrDefault(usuario => usuario.Id == id);
    }

    public List<Usuario> GetUserBy(string filtro)
    {        
        return _context
            .Usuarios.Where(u =>
                u.Nombre.Contains(filtro) || u.Apellido.Contains(filtro) || u.Email.Contains(filtro)
            )
            .ToList();
    }

    public List<Usuario> ListUsers()
    {
        return _context.Usuarios.ToList();
    }

    public Usuario CreateUser(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return usuario;
    }

    public void UpdateUser(int id, Usuario usuario)
    {        
        var usuarioToUpdate = GetOneUser(id);
        if (usuarioToUpdate != null)
        {
            usuarioToUpdate.Nombre        = usuario.Nombre;
            usuarioToUpdate.Apellido      = usuario.Apellido;
            usuarioToUpdate.NombreUsuario = usuario.NombreUsuario;
            usuarioToUpdate.Email         = usuario.Email;
            usuarioToUpdate.Contraseña    = usuario.Contraseña;
            _context.Usuarios.Update(usuarioToUpdate);
            _context.SaveChanges();
        }

    }

    public void DeleteUser(int id)
    {
        var usuario = GetOneUser(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }

    public bool UserExists(string email)
    {
        return _context.Usuarios.Any(u => u.Email == email);
    }

    public Usuario ValidateUser(string email, string password)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Contraseña == password);
    }

}




