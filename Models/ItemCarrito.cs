using BarbudosShop.Data;
using BarbudosShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ItemCarrito
{
    [Key]
    public int Id { get; set; }

    public string IdUsuario { get; set; } = string.Empty;

    [Required]
    public int Cantidad { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public ApplicationUser Usuario { get; set; }

    [ForeignKey(nameof(Producto))]
    public int IdProducto { get; set; }
    public Producto Producto { get; set; }
}