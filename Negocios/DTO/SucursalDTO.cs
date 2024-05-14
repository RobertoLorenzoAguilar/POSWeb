using Datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.DTO
{
    public class SucursalDTO
    {
        public int IdSucursal { get; set; }

        public string CalleDireccionSucursal { get; set; } = null!;

        public int NumeroDireccionSucursal { get; set; }

        public string RfcSucursal { get; set; } = null!;

        public string RazonSocialSucursal { get; set; } = null!;

        public string CentroCostoSucursal { get; set; } = null!;

        public string NombreSucursal { get; set; } = null!;

        public int IdEmpresa { get; set; }

        public bool Eliminado { get; set; }

        public string RazonSocialEmpresa { get; set; } = null!;
    }
}
