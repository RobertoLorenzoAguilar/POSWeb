using System;
using System.Collections.Generic;

namespace Datos.Models;

public partial class TblTelefonoSucursal
{
    public int IdSucursal { get; set; }

    public string TelefonoSucursal { get; set; } = null!;

    public virtual TblSucursal IdSucursalNavigation { get; set; } = null!;
}
