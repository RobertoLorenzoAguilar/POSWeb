using Datos.Models;
using Microsoft.EntityFrameworkCore;
using Negocios.Interfaces;
namespace Negocios.Clases
{
    public class LogicaEmpresa : IEmpresa
    {
        private PosDbContext db;
        public LogicaEmpresa(PosDbContext db)
        {
            this.db = db;

        }
        public (bool Success, string ErrorMessage) ActualizarEmpresa(TblEmpresa updatedEmpresa)
        {
            try
            {
                // Buscar el Empresa existente en la base de datos
                var existingEmpresa = db.TblEmpresas.Find(updatedEmpresa.IdEmpresa);

                if (existingEmpresa != null)
                {
                    // Se remueve la instancia del contexto para poder eliminarla
                    db.TblEmpresas.Remove(existingEmpresa);  
                    // Adjuntar el Empresa actualizado al contexto de Entity Framework
                    db.TblEmpresas.Attach(updatedEmpresa);

                    // Marcar la entidad como modificada
                    db.Entry(updatedEmpresa).State = EntityState.Modified;

                    // Guardar los cambios en la base de datos
                    db.SaveChanges();

                    return (true, ""); // Operación exitosa
                }
                else
                {
                    return (false, $"Empresa con ID {updatedEmpresa.IdEmpresa} no encontrado."); // Empresa no encontrado
                }
            }
            catch (Exception ex)
            {
                // Capturar y manejar la excepción                
                return (false, $"Error al actualizar el Empresa: {ex.Message}");
            }
        }
        public (bool Success, string ErrorMessage) EliminarEmpresa(int IdEmpresa)
        {
            try
            {
                var Empresa = db.TblEmpresas.FirstOrDefault(x => x.IdEmpresa == IdEmpresa);

                if (Empresa != null)
                {
                    Empresa.Eliminado = true; // Marcamos el Empresa como eliminado (eliminación lógica)
                    db.SaveChanges();
                    return (true, ""); // Devolver éxito sin mensaje de error
                }
                else
                {
                    return (false, $"Empresa con ID {IdEmpresa} no encontrado."); // Indicar que el Empresa no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (false, $"Error al eliminar el Empresa: {ex.Message}");
            }
        }
        public (TblEmpresa Empresa, bool Success, string ErrorMessage) GetEmpresa(int IdEmpresa)
        {
            try
            {
                TblEmpresa Empresa = db.TblEmpresas
                                       .FirstOrDefault(x => x.IdEmpresa == IdEmpresa && x.Eliminado == false);

                if (Empresa != null)
                {
                    return (Empresa, true, ""); // Devolver el Empresa encontrado y éxito sin mensaje de error
                }
                else
                {
                    return (null, false, $"Empresa con ID {IdEmpresa} no encontrado o está eliminado."); // Indicar que el Empresa no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener Empresa: {ex.Message}");
            }
        }
        public (List<TblEmpresa> Empresas, bool Success, string ErrorMessage) GetEmpresas()
        {
            try
            {
                List<TblEmpresa> Empresas = db.TblEmpresas
                                              .Where(x => x.Eliminado == false)
                                              .ToList();

                return (Empresas, true, ""); // Devolver la lista de Empresas y éxito sin mensaje de error
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener Empresas: {ex.Message}");
            }
        }
        public (bool Success, string ErrorMessage) GuardarEmpresa(TblEmpresa objEmpresa)
        {
            try
            {
                db.TblEmpresas.Add(objEmpresa);
                db.SaveChanges();
                return (true, ""); // Devolver éxito sin mensaje de error
            }
            catch (DbUpdateException ex)
            {
                // Capturar errores específicos de Entity Framework al guardar en la base de datos
                return (false, $"Error al guardar el Empresa: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                // Capturar cualquier otra excepción no controlada
                return (false, $"Error inesperado al guardar el Empresa: {ex.Message}");
            }
        }
    }
}
