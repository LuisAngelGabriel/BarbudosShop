using System.ComponentModel.DataAnnotations;

namespace BarbudosShop.Models
{
    public class DetallePedido
    {

        [Key]
        public int Id { get; set; }

        public int IdPedido { get; set; }
        public Pedido? Pedido { get; set; }

        [Required(ErrorMessage = "El ID del producto es obligatorio.")]
        public int IdProducto { get; set; }
        public Producto? Producto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }
    }
}
