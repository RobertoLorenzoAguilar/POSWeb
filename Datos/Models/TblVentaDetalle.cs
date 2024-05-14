using System;
using System.Collections.Generic;

namespace Datos.Models;

public partial class TblVentaDetalle
{
    public int IdVentaDetalle { get; set; }

    public int IdVenta { get; set; }

    public int IdProducto { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal { get; set; }

    public bool Eliminado { get; set; }

    public int Cantidad { get; set; }

    public virtual TblProducto IdProductoNavigation { get; set; } = null!;

    public virtual TblVentum IdVentaNavigation { get; set; } = null!;
}
