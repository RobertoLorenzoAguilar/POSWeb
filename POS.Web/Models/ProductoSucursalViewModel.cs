namespace POS.Web.Models
{
    public class ProductoSucursalViewModel
    {
        public decimal Precio { get; set; }

        public int IdProducto { get; set; }

        public int IdSucursal { get; set; }

        public bool Eliminado { get; set; }        
        
        public string? NombreSucursal { get; set; }
        public string? NombreProducto { get; set; }

    }
}
