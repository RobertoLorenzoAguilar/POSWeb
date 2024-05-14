using Datos.Models;
namespace Negocios.Interfaces
{
    public interface IProducto
    {

        (bool Success, string ErrorMessage) ActualizarProducto(TblProducto updatedProducto);
        (bool Success, string ErrorMessage) EliminarProducto(int IdProducto);
        (TblProducto Producto, bool Success, string ErrorMessage) GetProducto(int IdProducto);
        (List<TblProducto> Productos, bool Success, string ErrorMessage) GetProductos();
        (bool Success, string ErrorMessage) GuardarProducto(TblProducto objProducto);

        (List<TblProducto> Producto, bool Success, string ErrorMessage) BuscarProductos(string term);




    }
}
