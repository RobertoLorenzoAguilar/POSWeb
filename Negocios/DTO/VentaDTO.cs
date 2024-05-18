using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Negocios.DTO
{
    public class VentaDTO
    {
        public int IdVenta { get; set; }
        public string TipoVenta { get; set; } = null!;
        // Información del cliente
        public int IdCliente { get; set; }
        public string? RfcCliente { get; set; } = null;
        // Información del empleado
        public int IdEmpleado { get; set; }
        public string? UsuarioEmpleado { get; set; } = null;
        // Información de la Sucursal basandose en la sucursal en la cual esta vinculado el empleado
        public int IdSucursal { get; set; } = 0;
        public string? NombreSucursal { get; set; } = null;
        public DateTime FechaVenta { get; set; }  //se guarda al momento de registrar la venta
        public decimal TotalVenta { get; set; }

    }
}
