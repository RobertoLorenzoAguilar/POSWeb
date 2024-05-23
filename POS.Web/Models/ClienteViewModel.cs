using System.ComponentModel.DataAnnotations;

namespace POS.Web.Models
{
    public class ClienteViewModel
    {
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del cliente no puede tener más de 50 caracteres.")]
        public string NombreCliente { get; set; } = null!;

        [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido paterno no puede tener más de 50 caracteres.")]
        public string ApellidoPaterno { get; set; } = null!;

        [StringLength(50, ErrorMessage = "El apellido materno no puede tener más de 50 caracteres.")]
        public string? ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "El RFC es obligatorio.")]
        [StringLength(13, MinimumLength = 12, ErrorMessage = "El RFC debe tener entre 12 y 13 caracteres.")]
        public string RfcCliente { get; set; } = null!;

        [Required(ErrorMessage = "La calle de la dirección es obligatoria.")]
        [StringLength(100, ErrorMessage = "La calle de la dirección no puede tener más de 100 caracteres.")]
        public string CalleDireccionCliente { get; set; } = null!;

        [Required(ErrorMessage = "El número de la dirección es obligatorio.")]
        public int NumeroDirecionCiente { get; set; }

        [Required(ErrorMessage = "El ID de la empresa es obligatorio.")]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "El tipo de cliente es obligatorio.")]
        [StringLength(50, ErrorMessage = "El tipo de cliente no puede tener más de 50 caracteres.")]
        public string TipoCliente { get; set; } = null!;

        public string? RazonSocialEmpresa { get; set; }

        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100.")]
        public decimal Descuento { get; set; }

        [Required(ErrorMessage = "El ID del empleado es obligatorio.")]
        public int IdEmpleado { get; set; }

        public string? TipoUsuario { get; set; } // Asesor de Venta
    }

}