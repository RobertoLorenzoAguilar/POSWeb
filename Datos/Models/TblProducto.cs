using System;
using System.Collections.Generic;

namespace Datos.Models;

public partial class TblProducto
{
    public int IdProducto { get; set; }

    public string FolioProducto { get; set; } = null!;

    public string DescripcionProducto { get; set; } = null!;

    public string TipoProducto { get; set; } = null!;

    public bool Eliminado { get; set; }

    public bool Retencion { get; set; }

    public bool Traslado { get; set; }

    public string TipoImpuesto { get; set; } = null!;

    public bool TieneImpuesto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public virtual ICollection<TblProductoSucursal> TblProductoSucursals { get; set; } = new List<TblProductoSucursal>();

    public virtual ICollection<TblVentaDetalle> TblVentaDetalles { get; set; } = new List<TblVentaDetalle>();
}
