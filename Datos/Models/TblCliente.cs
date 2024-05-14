using System;
using System.Collections.Generic;

namespace Datos.Models;

public partial class TblCliente
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

    public bool Eliminado { get; set; }

    public decimal Descuento { get; set; }

    public int IdEmpleado { get; set; }

    public virtual TblEmpresa IdEmpresaNavigation { get; set; } = null!;

    public virtual ICollection<TblVentum> TblVenta { get; set; } = new List<TblVentum>();
}
