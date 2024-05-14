using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Negocios.DTO
{
    public class ProductoSucursalDTO
    {
        public int IdProducto { get; set; }

        public string NombreProducto { get; set; } = null!;
        public int IdSucursal { get; set; }

        public string NombreSucursal { get; set; } = null!;
        public decimal Precio { get; set; }

    }
}
