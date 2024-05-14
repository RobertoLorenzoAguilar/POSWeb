using Negocios.DTO;
using Datos.Models;
namespace Negocios.Interfaces
{
    public interface IEmpleado
    {

        (bool Success, string ErrorMessage) ActualizarEmpleado(TblEmpleado updatedEmpleado);
        (bool Success, string ErrorMessage) EliminarEmpleado(int IdEmpleado);
        (EmpleadoDTO Empleado, bool Success, string ErrorMessage) GetEmpleado(int IdEmpleado);
        (List<EmpleadoDTO> Empleados, bool Success, string ErrorMessage) GetEmpleados();
        (bool Success, string ErrorMessage) GuardarEmpleado(TblEmpleado objEmpleado);
        (List<EmpleadoDTO> Empleados, bool Success, string ErrorMessage) GetEmpleadosPorTipo(string tipoUsuario);


    }
}
