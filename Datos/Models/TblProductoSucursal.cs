using System;
using System.Collections.Generic;

namespace Datos.Models;

public partial class TblProductoSucursal
{
    public decimal Precio { get; set; }

    public int IdProducto { get; set; }

    public int IdSucursal { get; set; }

    public bool Eliminado { get; set; }

    public virtual TblProducto IdProductoNavigation { get; set; } = null!;

    public virtual TblSucursal IdSucursalNavigation { get; set; } = null!;
}
