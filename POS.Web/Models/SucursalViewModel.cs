namespace POS.Web.Models
{
    public class SucursalViewModel
    {
        public int IdSucursal { get; set; }

        public string CalleDireccionSucursal { get; set; } = null!;

        public int NumeroDireccionSucursal { get; set; }

        public string RfcSucursal { get; set; } = null!;

        public string RazonSocialSucursal { get; set; } = null!;

        public string CentroCostoSucursal { get; set; } = null!;

        public string NombreSucursal { get; set; } = null!;

        public int IdEmpresa { get; set; }

        public string? RazonSocialEmpresa { get; set; }

    }
}
