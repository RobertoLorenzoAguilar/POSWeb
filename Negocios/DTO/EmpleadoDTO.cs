using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Negocios.DTO
{
    public class EmpleadoDTO
    {
        public int IdEmpleado { get; set; }

        public string NombreEmpleado { get; set; } = null!;

        public string ApellidoPaterno { get; set; } = null!;

        public string ApellidoMaterno { get; set; } = null!;

        public string UsuarioSistema { get; set; } = null!;

        public string ContrasenaSistema { get; set; } = null!;

        public string Nss { get; set; } = null!;

        public int IdSucursal { get; set; }
        public string NombreSucursal { get; set; } = null!;

        public string TipoUsuario { get; set; } = null!;


    }
}
