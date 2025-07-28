using System.ComponentModel.DataAnnotations;

namespace BarbudosShop.Models
{
    public class ImagenProducto
    {

        [Key]
        public int ImagenId { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required]
        [StringLength(255)]
        public string UrlImagen { get; set; } = string.Empty;

        [Required]
        public int Orden { get; set; }

        public Producto Producto { get; set; }
    }
}
