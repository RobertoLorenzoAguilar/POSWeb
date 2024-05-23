using System.ComponentModel.DataAnnotations;

namespace POS.Web.Models
{
    public class SucursalViewModel
    {
        public int IdSucursal { get; set; }

        [Required(ErrorMessage = "La calle de la dirección es obligatoria.")]
        public string CalleDireccionSucursal { get; set; } = null!;

        [Required(ErrorMessage = "El número de la dirección es obligatorio.")]
        public int NumeroDireccionSucursal { get; set; }

        [Required(ErrorMessage = "El RFC es obligatorio.")]
        [StringLength(13, MinimumLength = 12, ErrorMessage = "El RFC debe tener entre 12 y 13 caracteres.")]
        public string RfcSucursal { get; set; } = null!;

        [Required(ErrorMessage = "La Razón Social es obligatoria.")]
        public string RazonSocialSucursal { get; set; } = null!;

        [Required(ErrorMessage = "El Centro de Costos es obligatorio.")]
        public string CentroCostoSucursal { get; set; } = null!;

        [Required(ErrorMessage = "El Nombre de Sucursal es obligatorio.")]
        public string NombreSucursal { get; set; } = null!;

        public int IdEmpresa { get; set; }

        public string? RazonSocialEmpresa { get; set; }

    }
}
