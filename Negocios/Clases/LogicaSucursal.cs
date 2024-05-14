using Datos.Models;
using Microsoft.EntityFrameworkCore;
using Negocios.DTO;
using Negocios.Interfaces;
namespace Negocios.Clases
{
    public class LogicaSucursal : ISucursal
    {
        private PosDbContext db;
        public LogicaSucursal(PosDbContext db)
        {
            this.db = db;

        }
        public (bool Success, string ErrorMessage) ActualizarSucursal(TblSucursal updatedSucursal)
        {
            try
            {
                // Buscar el Sucursal existente en la base de datos
                var existingSucursal = db.TblSucursals.Find(updatedSucursal.IdSucursal);

                if (existingSucursal != null)
                {
                    // Se remueve la instancia del contexto para poder eliminarla
                    db.TblSucursals.Remove(existingSucursal);  
                    // Adjuntar el Sucursal actualizado al contexto de Entity Framework
                    db.TblSucursals.Attach(updatedSucursal);

                    // Marcar la entidad como modificada
                    db.Entry(updatedSucursal).State = EntityState.Modified;

                    // Guardar los cambios en la base de datos
                    db.SaveChanges();

                    return (true, ""); // Operación exitosa
                }
                else
                {
                    return (false, $"Sucursal con ID {updatedSucursal.IdSucursal} no encontrado."); // Sucursal no encontrado
                }
            }
            catch (Exception ex)
            {
                // Capturar y manejar la excepción                
                return (false, $"Error al actualizar el Sucursal: {ex.Message}");
            }
        }
        public (bool Success, string ErrorMessage) EliminarSucursal(int IdSucursal)
        {
            try
            {
                var Sucursal = db.TblSucursals.FirstOrDefault(x => x.IdSucursal == IdSucursal);

                if (Sucursal != null)
                {
                    Sucursal.Eliminado = true; // Marcamos el Sucursal como eliminado (eliminación lógica)
                    db.SaveChanges();
                    return (true, ""); // Devolver éxito sin mensaje de error
                }
                else
                {
                    return (false, $"Sucursal con ID {IdSucursal} no encontrado."); // Indicar que el Sucursal no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (false, $"Error al eliminar el Sucursal: {ex.Message}");
            }
        }
        public (SucursalDTO Sucursal, bool Success, string ErrorMessage) GetSucursal(int IdSucursal)
        {
            try
            {
                TblSucursal Sucursal = db.TblSucursals.Include(e=> e.IdEmpresaNavigation)
                                       .FirstOrDefault(x => x.IdSucursal == IdSucursal && x.Eliminado == false);


                SucursalDTO sucursalDto = new SucursalDTO();
                sucursalDto.IdSucursal = Sucursal.IdSucursal;
                sucursalDto.CalleDireccionSucursal = Sucursal.CalleDireccionSucursal;
                sucursalDto.NumeroDireccionSucursal = Sucursal.NumeroDireccionSucursal;
                sucursalDto.RfcSucursal = Sucursal.RfcSucursal;
                sucursalDto.RazonSocialSucursal = Sucursal.RazonSocialSucursal;
                sucursalDto.CentroCostoSucursal = Sucursal.CentroCostoSucursal;
                sucursalDto.NombreSucursal = Sucursal.NombreSucursal;
                sucursalDto.IdEmpresa = Sucursal.IdEmpresa;
                sucursalDto.RazonSocialEmpresa = Sucursal.IdEmpresaNavigation.RazonSocialEmpresa;

                if (Sucursal != null)
                {
                    return (sucursalDto, true, ""); // Devolver el Sucursal encontrado y éxito sin mensaje de error
                }
                else
                {
                    return (null, false, $"Sucursal con ID {IdSucursal} no encontrado o está eliminado."); // Indicar que el Sucursal no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener Sucursal: {ex.Message}");
            }
        }
        public (List<SucursalDTO> Sucursals, bool Success, string ErrorMessage) GetSucursals()
        {
            try
            {
                List<TblSucursal> Sucursals = db.TblSucursals
                    .Where(x => x.Eliminado == false  && x.IdEmpresaNavigation.Eliminado == false) // Agregar condiciones adicionales
                    .Include(e => e.IdEmpresaNavigation) // Incluir la propiedad de navegación IdEmpresaNavigation
                    .ToList();

                List<SucursalDTO> sucursalesDto = new List<SucursalDTO>();
                foreach (var sucursal in Sucursals)
                {
                    SucursalDTO sucursalDto = new SucursalDTO();
                    sucursalDto.IdSucursal = sucursal.IdSucursal;
                    sucursalDto.CalleDireccionSucursal = sucursal.CalleDireccionSucursal;
                    sucursalDto.NumeroDireccionSucursal = sucursal.NumeroDireccionSucursal;
                    sucursalDto.RfcSucursal = sucursal.RfcSucursal;
                    sucursalDto.RazonSocialSucursal = sucursal.RazonSocialSucursal;
                    sucursalDto.CentroCostoSucursal = sucursal.CentroCostoSucursal;
                    sucursalDto.NombreSucursal = sucursal.NombreSucursal;
                    sucursalDto.IdEmpresa = sucursal.IdEmpresa;
                    sucursalDto.RazonSocialEmpresa = sucursal.IdEmpresaNavigation.RazonSocialEmpresa;

                    // Puedes asignar otros valores según sea necesario

                    sucursalesDto.Add(sucursalDto);
                }




                return (sucursalesDto, true, ""); // Devolver la lista de Sucursals y éxito sin mensaje de error
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener Sucursals: {ex.Message}");
            }
        }
        public (bool Success, string ErrorMessage) GuardarSucursal(TblSucursal objSucursal)
        {
            try
            {
                db.TblSucursals.Add(objSucursal);
                db.SaveChanges();
                return (true, ""); // Devolver éxito sin mensaje de error
            }
            catch (DbUpdateException ex)
            {
                // Capturar errores específicos de Entity Framework al guardar en la base de datos
                return (false, $"Error al guardar el Sucursal: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                // Capturar cualquier otra excepción no controlada
                return (false, $"Error inesperado al guardar el Sucursal: {ex.Message}");
            }
        }
    }
}
