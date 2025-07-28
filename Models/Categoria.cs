using System.ComponentModel.DataAnnotations;

namespace BarbudosShop.Models
{
    public class Categoria
    {

        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public ICollection<Producto> Productos { get; set; }
    }
}
