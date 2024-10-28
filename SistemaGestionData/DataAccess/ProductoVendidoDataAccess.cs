using Microsoft.EntityFrameworkCore;
using SistemaGestionData.Context;
using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionData.DataAccess;
public class ProductoVendidoDataAccess
{
    private CoderhouseContext _context;

    public ProductoVendidoDataAccess()
    {
        _context = new CoderhouseContext();
    }

    public ProductosVendidos? ObtenerProductoVendido(int id)
    {
        return _context.ProductosVendidos.FirstOrDefault(productov => productov.Id == id);
    }

    public List<ProductosVendidos> ListarProductosVendidos()
    {    
        var productosVendidos =  (from pv in _context.ProductosVendidos
                                       join p in _context.Productos on pv.IdProducto equals p.Id
                                       select new ProductosVendidos
                                       {
                                           Id = pv.Id,
                                           IdProducto = pv.IdProducto,
                                           NombreProducto = p.Descripcion,
                                           Cantidad = pv.Cantidad,
                                           IdVenta = pv.IdVenta
                                       }).ToList();

        return productosVendidos;
    }

    public ProductosVendidos CrearProductoVendido(ProductosVendidos productov)
    {
        _context.ProductosVendidos.Add(productov);
        _context.SaveChanges();        
        return productov;
    }

    public void ModificarProductoVendido(int id, ProductosVendidos productov)
    {
        var productovToUpdate = ObtenerProductoVendido(id);
        if (productovToUpdate != null)
        {
            productovToUpdate.Id         = productov.Id;
            productovToUpdate.IdProducto = productov.IdProducto;
            productovToUpdate.Stock      = productov.Stock;
            productovToUpdate.IdVenta    = productov.IdVenta;            
            _context.ProductosVendidos.Update(productovToUpdate);
            _context.SaveChanges();
        }

    }

    public void EliminarProductoVendido(int id)
    {
        var productov = ObtenerProductoVendido(id);
        if (productov != null)
        {
            _context.ProductosVendidos.Remove(productov);
            _context.SaveChanges();
        }
    }

}