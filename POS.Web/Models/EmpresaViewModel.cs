namespace POS.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EmpresaViewModel
    {
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "La razón social es obligatoria.")]
        public string RazonSocialEmpresa { get; set; } = null!;

        [Required(ErrorMessage = "La calle de la dirección es obligatoria.")]
        public string CalleDireccionEmpresa { get; set; } = null!;

        [Required(ErrorMessage = "El número de la dirección es obligatorio.")]
        public int NumeroDireccionEmpresa { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        public string CiudadEmpresa { get; set; } = null!;

        [Required(ErrorMessage = "El RFC es obligatorio.")]
        [StringLength(13, MinimumLength = 12, ErrorMessage = "El RFC debe tener entre 12 y 13 caracteres.")]
        public string RfcEmpresa { get; set; } = null!;
    }

}
