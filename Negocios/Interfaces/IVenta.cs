using Datos.Models;
namespace Negocios.Interfaces
{
    public interface IVenta
    {

        (bool Success, string ErrorMessage) ActualizarVenta(TblVentum updatedVenta);
        (bool Success, string ErrorMessage) EliminarVenta(int IdVenta);
        (TblVentum Venta, bool Success, string ErrorMessage) GetVenta(int IdVenta);
        (List<TblVentum> Ventas, bool Success, string ErrorMessage) GetVentas();
        (bool Success, string ErrorMessage) GuardarVenta(TblVentum objVenta);


    }
}
