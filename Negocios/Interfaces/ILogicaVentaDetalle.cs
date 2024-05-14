using Datos.Models;
namespace Negocios.Interfaces
{
    public interface IVentaDetalle
    {

        (bool Success, string ErrorMessage) ActualizarVentaDetalle(TblVentaDetalle updatedVentaDetalle);
        (bool Success, string ErrorMessage) EliminarVentaDetalle(int IdVentaDetalle);
        (TblVentaDetalle VentaDetalle, bool Success, string ErrorMessage) GetVentaDetalle(int IdVentaDetalle);
        (List<TblVentaDetalle> VentaDetalles, bool Success, string ErrorMessage) GetVentaDetalles();
        (bool Success, string ErrorMessage) GuardarVentaDetalle(TblVentaDetalle objVentaDetalle);


    }
}
