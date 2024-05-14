namespace POS.Web.Models
{
    public class EmpresaViewModel
    {
        public int IdEmpresa { get; set; }

        public string RazonSocialEmpresa { get; set; } = null!;

        public string CalleDireccionEmpresa { get; set; } = null!;

        public int NumeroDireccionEmpresa { get; set; }

        public string CiudadEmpresa { get; set; } = null!;

        public string RfcEmpresa { get; set; } = null!;        
    }
}
