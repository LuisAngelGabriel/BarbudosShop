// Models/DireccionEnvioViewModel.cs

using System.ComponentModel.DataAnnotations;

namespace BarbudosShop.Models
{
    public class DireccionEnvioViewModel
    {
        [Required(ErrorMessage = "La calle es obligatoria.")]
        public string Calle { get; set; } = string.Empty;

        [Required(ErrorMessage = "El sector es obligatorio.")]
        public string Sector { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        public string Ciudad { get; set; } = string.Empty;

        [Required(ErrorMessage = "La provincia es obligatoria.")]
        public string Provincia { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener 10 dígitos.")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de tarjeta es obligatorio.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "El número de tarjeta debe tener 16 dígitos.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "El número de tarjeta debe contener solo números y tener 16 dígitos.")]
        public string NumeroTarjeta { get; set; } = string.Empty;


        [Required(ErrorMessage = "El CVC es obligatorio.")]
        [StringLength(3, ErrorMessage = "El CVC debe tener 3 dígitos.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "El CVC debe ser de 3 dígitos.")]
        public string Cvc { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Formato de fecha inválido. Use MM/AA.")]
        public string FechaVencimiento { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre en la tarjeta es obligatorio.")]
        public string NombreEnTarjeta { get; set; } = string.Empty;
    }
}