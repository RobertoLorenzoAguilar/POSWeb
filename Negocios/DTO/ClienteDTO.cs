using Datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.DTO
{
    public class ClienteDTO
    {
        public int IdCliente { get; set; }

        public string ApellidoPaterno { get; set; } = null!;

        public string ApellidoMaterno { get; set; } = null!;

        public string NombreCliente { get; set; } = null!;

        public string RfcCliente { get; set; } = null!;

        public string CalleDireccionCliente { get; set; } = null!;

        public int NumeroDirecionCiente { get; set; }

        public int IdEmpresa { get; set; }

        public string TipoCliente { get; set; } = null!;
        public string RazonSocialEmpresa { get; set; } = null!;
        public decimal Descuento { get; set; }
        public int IdEmpleado { get; set; }
        public string? TipoUsuario { get; set; }  // Asesor de Venta
    }
}
