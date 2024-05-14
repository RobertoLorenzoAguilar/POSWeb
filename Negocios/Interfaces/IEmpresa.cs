using Datos.Models;
namespace Negocios.Interfaces
{
    public interface IEmpresa
    {

        (bool Success, string ErrorMessage) ActualizarEmpresa(TblEmpresa updatedEmpresa);
        (bool Success, string ErrorMessage) EliminarEmpresa(int IdEmpresa);
        (TblEmpresa Empresa, bool Success, string ErrorMessage) GetEmpresa(int IdEmpresa);
        (List<TblEmpresa> Empresas, bool Success, string ErrorMessage) GetEmpresas();
        (bool Success, string ErrorMessage) GuardarEmpresa(TblEmpresa objEmpresa);


    }
}
