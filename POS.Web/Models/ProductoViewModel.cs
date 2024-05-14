namespace POS.Web.Models
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }

        public string FolioProducto { get; set; } = null!;
        public string NombreProducto { get; set; } = null!;

        public string DescripcionProducto { get; set; } = null!;

        public string TipoProducto { get; set; } = null!;

        public bool Eliminado { get; set; }

        //public decimal Porcentaje { get; set; }

        public bool Retencion { get; set; }

        public bool Traslado { get; set; }

        public string TipoImpuesto { get; set; } = null!;

        public bool TieneImpuesto { get; set; }
    }
}
