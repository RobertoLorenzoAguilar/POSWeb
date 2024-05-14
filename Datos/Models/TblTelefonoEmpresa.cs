using System;
using System.Collections.Generic;

namespace Datos.Models;

public partial class TblTelefonoEmpresa
{
    public int IdEmpresa { get; set; }

    public string TelefonoEmpresa { get; set; } = null!;

    public virtual TblEmpresa IdEmpresaNavigation { get; set; } = null!;
}
