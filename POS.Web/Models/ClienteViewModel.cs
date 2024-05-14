using System.ComponentModel.DataAnnotations;

namespace POS.Web.Models
{
    public class ClienteViewModel
    {
        public int IdCliente { get; set; }
        //[StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
        public string NombreCliente { get; set; } = null!;
        //[StringLength(8, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string ApellidoPaterno { get; set; } = null!;

        public string ApellidoMaterno { get; set; } = null!;

        public string RfcCliente { get; set; } = null!;

        public string CalleDireccionCliente { get; set; } = null!;
        //[Required(ErrorMessage = "El numero de la calle es obligatorio.")]
        public int NumeroDirecionCiente { get; set; }

        public int IdEmpresa { get; set; }

        public string TipoCliente { get; set; } = null!;
        public string? RazonSocialEmpresa { get; set; }
        public decimal Descuento { get; set; }
        public int IdEmpleado { get; set; }

        public string? TipoUsuario { get; set; } // Asesor de Venta

    }
}
