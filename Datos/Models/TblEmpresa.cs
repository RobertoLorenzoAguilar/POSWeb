using System;
using System.Collections.Generic;

namespace Datos.Models;

public partial class TblEmpresa
{
    public int IdEmpresa { get; set; }

    public string RazonSocialEmpresa { get; set; } = null!;

    public string CalleDireccionEmpresa { get; set; } = null!;

    public int NumeroDireccionEmpresa { get; set; }

    public string CiudadEmpresa { get; set; } = null!;

    public string RfcEmpresa { get; set; } = null!;

    public bool Eliminado { get; set; }

    public virtual ICollection<TblCliente> TblClientes { get; set; } = new List<TblCliente>();

    public virtual ICollection<TblSucursal> TblSucursals { get; set; } = new List<TblSucursal>();

    public virtual ICollection<TblTelefonoEmpresa> TblTelefonoEmpresas { get; set; } = new List<TblTelefonoEmpresa>();
}
