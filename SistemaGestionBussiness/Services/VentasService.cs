using SistemaGestionData.Context;
using SistemaGestionData.DataAccess;
using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBussiness.Services;

public class VentasService
{
    
    private VentasDataAccess _ventasDataAccess;

    public VentasService(VentasDataAccess ventasDataAccess)
    {
        _ventasDataAccess = ventasDataAccess;
    }

    public Venta? ObtenerVenta(int id)
    {
        return _ventasDataAccess.ObtenerVenta(id);
    }

    public List<Venta> ListarVentas()
    {
        return _ventasDataAccess.ListarVentas();
    }

    public Venta CrearVenta(Venta venta)
    {
        return _ventasDataAccess.CrearVenta(venta);        
    }

    public void ModificarVenta(int id, Venta venta)
    {
        _ventasDataAccess.ModificarVenta(id, venta);       
    }

    public void EliminarVenta(int id)
    {
        _ventasDataAccess.EliminarVenta(id);        
    }
}

