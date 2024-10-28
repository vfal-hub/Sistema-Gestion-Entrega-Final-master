using Microsoft.EntityFrameworkCore;
using SistemaGestionData.Context;
using SistemaGestionEntities;

namespace SistemaGestionData.DataAccess;

public class VentasDataAccess
{
    private CoderhouseContext _context;

    public VentasDataAccess()
    {
        _context = new CoderhouseContext();
    }

    public Venta? ObtenerVenta(int id)
    {
        return _context.Ventas.FirstOrDefault(venta => venta.Id == id);
    }

    public List<Venta> ListarVentas()
    {
        return _context.Ventas.ToList();
    }

    public Venta CrearVenta(Venta venta)
    {
        _context.Ventas.Add(venta);
        _context.SaveChanges();
        return venta;
    }

    public void ModificarVenta(int id, Venta venta)
    {
        var ventaToUpdate = ObtenerVenta(id);
        if (ventaToUpdate != null)
        {
            ventaToUpdate.Id          = venta.Id;
            ventaToUpdate.Comentarios = venta.Comentarios;
            ventaToUpdate.Usuario   = venta.Usuario;            
            _context.Ventas.Update(ventaToUpdate);            
            _context.SaveChanges();
        }
    }

    public void EliminarVenta(int id)
    {
        var venta = ObtenerVenta(id);
        if (venta != null)
        {
            _context.Ventas.Remove(venta);
            _context.SaveChanges();
        }
    }
}

