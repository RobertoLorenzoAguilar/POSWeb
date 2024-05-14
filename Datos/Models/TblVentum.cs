using System;
using System.Collections.Generic;

namespace Datos.Models;

public partial class TblVentum
{
    public int IdVenta { get; set; }

    public string TipoVenta { get; set; } = null!;

    public int IdCliente { get; set; }

    public int IdEmpleado { get; set; }

    public DateTime FechaVenta { get; set; }

    public decimal TotalVenta { get; set; }

    public bool Eliminado { get; set; }

    public virtual TblCliente IdClienteNavigation { get; set; } = null!;

    public virtual TblEmpleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual ICollection<TblVentaDetalle> TblVentaDetalles { get; set; } = new List<TblVentaDetalle>();
}
