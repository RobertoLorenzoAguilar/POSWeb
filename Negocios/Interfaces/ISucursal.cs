using Datos.Models;
using Negocios.DTO;
namespace Negocios.Interfaces
{
    public interface ISucursal
    {

        (bool Success, string ErrorMessage) ActualizarSucursal(TblSucursal updatedSucursal);
        (bool Success, string ErrorMessage) EliminarSucursal(int IdSucursal);
        (SucursalDTO Sucursal, bool Success, string ErrorMessage) GetSucursal(int IdSucursal);
        (List<SucursalDTO> Sucursals, bool Success, string ErrorMessage) GetSucursals();
        (bool Success, string ErrorMessage) GuardarSucursal(TblSucursal objSucursal);


    }
}
