using BarbudosShop.Data;
using System; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BarbudosShop.Models
{
    public class ItemCarrito
    {
        [Key]

        public int Id { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        public string IdUsuario { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public ApplicationUser Usuario { get; set; }


    }
}