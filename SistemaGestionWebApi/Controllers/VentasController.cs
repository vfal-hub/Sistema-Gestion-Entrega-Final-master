using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness.Services;
using SistemaGestionEntities;

namespace SistemaGestionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly ILogger<VentasController> _logger;
        private readonly VentasService _ventasService;

        public VentasController(ILogger<VentasController> logger, VentasService ventasService)
        {
            _logger = logger;
            _ventasService = ventasService;
        }

        [HttpGet(Name = "Get Ventas")]
        public ActionResult<List<Venta>> ListarVentas()
        {
            return _ventasService.ListarVentas();
        }

        [HttpGet("{id}")]
        public ActionResult<Venta> ObtenerVenta(int id)
        {
            var producto = _ventasService.ObtenerVenta(id);
            if (producto is null)
            {
                return NotFound();
            }
            return producto;
        }

        [HttpPost]
        public ActionResult<Venta> CrearVentas([FromBody] Venta venta)
        {
            var ventaCreada = _ventasService.CrearVenta(venta);
            return CreatedAtAction(nameof(CrearVentas), new { id = ventaCreada.Id }, venta);
        }

        [HttpPut("{id}")]
        public ActionResult ModificarVentas([FromRoute(Name = "id")] int id, [FromBody] Venta venta)
        {
            _ventasService.ModificarVenta(id, venta);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult EliminarVentas([FromRoute(Name = "id")] int id)
        {
            _ventasService.EliminarVenta(id);
            return NoContent();
        }

    }
}
