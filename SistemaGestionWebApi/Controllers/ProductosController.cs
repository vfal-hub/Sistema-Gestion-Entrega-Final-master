using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness.Services;
using SistemaGestionEntities;

namespace SistemaGestionWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly ILogger<ProductosController> _logger;
    private readonly ProductosService _productosService;

    public ProductosController(ILogger<ProductosController> logger, ProductosService productosService)
    {
        _logger = logger;
        _productosService = productosService;
    }


    [HttpGet(Name = "Get Productos")]
    public ActionResult<List<Producto>> ListProducts([FromQuery(Name = "filtro")] string? filtro)
    {
        if (filtro == null)
        {
            return _productosService.ListProducts();
        }
        return _productosService.GetProductBy(filtro);
    }

    [HttpGet("{id}")]
    public ActionResult<Producto> GetOneProduct(int id)
    {        
        var producto = _productosService.GetOneProduct(id);
        if (producto is null)
        {
            return NotFound();
        }
        return producto;
    }

    [HttpPost]
    public ActionResult<Producto> GetOneProduct([FromBody] Producto producto)
    {
        var productoCreado = _productosService.InsertProduct(producto);
        return CreatedAtAction(nameof(GetOneProduct), new { id = productoCreado.Id }, producto);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateProduct([FromRoute(Name = "id")] int id, [FromBody] Producto producto)
    {
        _productosService.UpdateProduct(id, producto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteProduct([FromRoute(Name = "id")] int id)
    {
        _productosService.DeleteProduct(id);
        return NoContent();
    }

    [HttpPut("fix-total")]
    public ActionResult FixTotalProductos()
    {
        _productosService.UpdateTotalProducts();
        return NoContent();
    }

}
