using Datos.Models;
using Negocios.DTO;
namespace Negocios.Interfaces
{
    public interface ICliente
    {

        (bool Success, string ErrorMessage) ActualizarCliente(TblCliente updatedCliente);
        (bool Success, string ErrorMessage) EliminarCliente(int IdCliente);
        (ClienteDTO Cliente, bool Success, string ErrorMessage) GetCliente(int IdCliente);
        (List<ClienteDTO> Clientes, bool Success, string ErrorMessage) GetClientes();
        (bool Success, string ErrorMessage) GuardarCliente(TblCliente objCliente);


    }
}
