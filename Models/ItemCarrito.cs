using System.ComponentModel.DataAnnotations;

namespace BarbudosShop.Models
{
    public class ItemCarrito
    {
        [Key]
        public int ItemCarritoId { get; set; }

        [Required]
        public int ProductoId { get; set; }

        public Producto Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public DateTime FechaAgregado { get; set; } = DateTime.UtcNow;
    }
}
