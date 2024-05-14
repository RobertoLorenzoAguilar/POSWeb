using System;
using System.Collections.Generic;

namespace Datos.Models;

public partial class TblSucursal
{
    public int IdSucursal { get; set; }

    public string CalleDireccionSucursal { get; set; } = null!;

    public int NumeroDireccionSucursal { get; set; }

    public string RfcSucursal { get; set; } = null!;

    public string RazonSocialSucursal { get; set; } = null!;

    public string CentroCostoSucursal { get; set; } = null!;

    public string NombreSucursal { get; set; } = null!;

    public int IdEmpresa { get; set; }

    public bool Eliminado { get; set; }

    public virtual TblEmpresa IdEmpresaNavigation { get; set; } = null!;

    public virtual ICollection<TblEmpleado> TblEmpleados { get; set; } = new List<TblEmpleado>();

    public virtual ICollection<TblProductoSucursal> TblProductoSucursals { get; set; } = new List<TblProductoSucursal>();

    public virtual ICollection<TblTelefonoSucursal> TblTelefonoSucursals { get; set; } = new List<TblTelefonoSucursal>();
}
