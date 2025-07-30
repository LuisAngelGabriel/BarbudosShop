using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BarbudosShop.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        [Required]
        public int CantidadEnStock { get; set; }

        public ICollection<ImagenProducto> Imagenes { get; set; }
    }
}