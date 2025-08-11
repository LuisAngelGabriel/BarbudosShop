using System.ComponentModel.DataAnnotations;

namespace BarbudosShop.Models
{
    public class Sugerencia
    {
        public int SugerenciaId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}