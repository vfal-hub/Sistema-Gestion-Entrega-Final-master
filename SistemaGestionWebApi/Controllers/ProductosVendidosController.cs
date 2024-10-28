using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness.Services;
using SistemaGestionEntities;

namespace SistemaGestionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosVendidosController : ControllerBase
    {
        private readonly ILogger<ProductosVendidosController> _logger;
        private readonly ProductosVendidosService _productosVendidosService;

        public ProductosVendidosController(ILogger<ProductosVendidosController> logger, ProductosVendidosService productosVendidosService)
        {
            _logger = logger;
            _productosVendidosService = productosVendidosService;
        }

        [HttpGet(Name = "Get Productos Vendidos")]
        public ActionResult<List<ProductosVendidos>> ListarProductosVendidos()
        {
            return _productosVendidosService.ListarProductosVendidos();
        }

        [HttpGet("{id}")]
        public ActionResult<ProductosVendidos> ObtenerProductoVendido(int id)
        {            
            var producto = _productosVendidosService.ObtenerProductoVendido(id);
            if (producto is null)
            {
                return NotFound();
            }
            return producto;
        }

        [HttpPost]
        public ActionResult<ProductosVendidos> CrearProductosVendidos([FromBody] ProductosVendidos productosVendidos)
        {
            var productosVendidosCreado = _productosVendidosService.CrearProductoVendido(productosVendidos);
            return CreatedAtAction(nameof(ObtenerProductoVendido), new { id = productosVendidosCreado.Id }, productosVendidos);
        }

        [HttpPut("{id}")]
        public ActionResult ModificarProductosVendidos([FromRoute(Name = "id")] int id, [FromBody] ProductosVendidos productosVendidos)
        {
            _productosVendidosService.ModificarProductoVendido(id, productosVendidos);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProductosVendidos([FromRoute(Name = "id")] int id)
        {
            _productosVendidosService.EliminarProductoVendido(id);
            return NoContent();
        }
    }
}