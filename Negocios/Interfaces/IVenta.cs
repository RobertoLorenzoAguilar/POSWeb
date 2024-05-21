using Datos.Models;
using Negocios.DTO;
namespace Negocios.Interfaces
{
    public interface IVenta
    {

        (bool Success, string ErrorMessage) ActualizarVenta(TblVentum updatedVenta);
        (bool Success, string ErrorMessage) EliminarVenta(int IdVenta);
        (VentaDTO Venta, bool Success, string ErrorMessage) GetVenta(int IdVenta);        
        (List<VentaDTO> Ventas, bool Success, string ErrorMessage) GetVentas();
        (bool Success, string ErrorMessage) GuardarVenta(VentaDTO objVenta);


    }
}
