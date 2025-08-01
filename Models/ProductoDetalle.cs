using System.ComponentModel.DataAnnotations;

namespace BarbudosShop.Models
{
    public class ProductoDetalle
    {

        [Key]
        public int DetalleId { get; set; }

        [Required]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        // Común a todos
        public string Marca { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        // Solo para máquinas de afeitar o kits
        public bool IncluyeKit { get; set; } // true si incluye accesorios
        public int NumeroPiezas { get; set; } // número de piezas del kit

        // Para geles, tintes y tratamientos
        public int PesoML { get; set; } // en mililitros

        // Para indumentaria
        public string Talla { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}
