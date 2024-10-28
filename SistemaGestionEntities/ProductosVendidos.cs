using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEntities;

public class ProductosVendidos
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo IdProducto es requerido.")]    
    public int? IdProducto { get; set; }

    public string? NombreProducto { get; set; }

    [Required(ErrorMessage = "El campo Stock es requerido.")]    
    public int Stock { get; set; }

    [Required(ErrorMessage = "El campo Cantidad es requerido.")]
    public decimal Cantidad { get; set; }

    [Required(ErrorMessage = "El campo IdVenta es requerido.")]
    [Range(1, int.MaxValue, ErrorMessage = "El campo IdVenta debe ser mayor a 0.")]
    public int? IdVenta { get; set; }

}
