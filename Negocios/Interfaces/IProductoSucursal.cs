using Negocios.DTO;
using Datos.Models;
namespace Negocios.Interfaces
{
    public interface IProductoSucursal
    {

        (bool Success, string ErrorMessage) ActualizarProductoSucursal(TblProductoSucursal updatedProductoSucursal);
        (bool Success, string ErrorMessage) EliminarProductoSucursal(int IdProductoSucursal);
        (ProductoSucursalDTO ProductoSucursal, bool Success, string ErrorMessage) GetProductoSucursal(int IdProductoSucursal);
        (List<ProductoSucursalDTO> ProductoSucursals, bool Success, string ErrorMessage) GetProductoSucursals();
        (bool Success, string ErrorMessage) GuardarProductoSucursal(TblProductoSucursal objProductoSucursal);       

    }
}
