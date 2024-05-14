using System;
using System.Collections.Generic;

namespace Datos.Models;

public partial class TblEmpleado
{
    public int IdEmpleado { get; set; }

    public string NombreEmpleado { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string UsuarioSistema { get; set; } = null!;

    public string ContrasenaSistema { get; set; } = null!;

    public string Nss { get; set; } = null!;

    public int IdSucursal { get; set; }

    public string TipoUsuario { get; set; } = null!;

    public bool Eliminado { get; set; }

    public virtual TblSucursal IdSucursalNavigation { get; set; } = null!;

    public virtual ICollection<TblVentum> TblVenta { get; set; } = new List<TblVentum>();
}
