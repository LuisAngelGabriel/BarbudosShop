using System.ComponentModel.DataAnnotations;

namespace BarbudosShop.Models
{
    public class Pedido
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
        public string IdUsuario { get; set; } = string.Empty;

        public DateTime FechaPedido { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La calle y número son obligatorios.")]
        [StringLength(100, ErrorMessage = "La calle no puede exceder los 100 caracteres.")]
        public string Calle { get; set; } = string.Empty;

        [Required(ErrorMessage = "El sector es obligatorio.")]
        [StringLength(50, ErrorMessage = "El sector no puede exceder los 50 caracteres.")]
        public string Sector { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        [StringLength(50, ErrorMessage = "La ciudad no puede exceder los 50 caracteres.")]
        public string Ciudad { get; set; } = string.Empty;

        [Required(ErrorMessage = "La provincia es obligatoria.")]
        [StringLength(50, ErrorMessage = "La provincia no puede exceder los 50 caracteres.")]
        public string Provincia { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El formato del teléfono es inválido.")]
        public string Telefono { get; set; } = string.Empty;

        public decimal Total { get; set; }

        public List<DetallePedido>? Detalles { get; set; }
    }
}
