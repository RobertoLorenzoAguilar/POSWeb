using Datos.Models;
using Microsoft.EntityFrameworkCore;
using Negocios.DTO;
using Negocios.Interfaces;
namespace Negocios.Clases
{
    public class LogicaEmpleado : IEmpleado
    {
        private PosDbContext db;
        public LogicaEmpleado(PosDbContext db)
        {
            this.db = db;

        }
        public (bool Success, string ErrorMessage) ActualizarEmpleado(TblEmpleado updatedEmpleado)
        {
            try
            {
                // Buscar el Empleado existente en la base de datos
                var existingEmpleado = db.TblEmpleados.Find(updatedEmpleado.IdEmpleado);

                if (existingEmpleado != null)
                {
                    // Se remueve la instancia del contexto para poder eliminarla
                    db.TblEmpleados.Remove(existingEmpleado);  
                    // Adjuntar el Empleado actualizado al contexto de Entity Framework
                    db.TblEmpleados.Attach(updatedEmpleado);

                    // Marcar la entidad como modificada
                    db.Entry(updatedEmpleado).State = EntityState.Modified;

                    // Guardar los cambios en la base de datos
                    db.SaveChanges();

                    return (true, ""); // Operación exitosa
                }
                else
                {
                    return (false, $"Empleado con ID {updatedEmpleado.IdEmpleado} no encontrado."); // Empleado no encontrado
                }
            }
            catch (Exception ex)
            {
                // Capturar y manejar la excepción                
                return (false, $"Error al actualizar el Empleado: {ex.InnerException.Message}");
            }
        }




        public (bool Success, string ErrorMessage) EliminarEmpleado(int IdEmpleado)
        {
            try
            {
                var Empleado = db.TblEmpleados.FirstOrDefault(x => x.IdEmpleado == IdEmpleado);

                if (Empleado != null)
                {
                    Empleado.Eliminado = true; // Marcamos el Empleado como eliminado (eliminación lógica)
                    db.SaveChanges();
                    return (true, ""); // Devolver éxito sin mensaje de error
                }
                else
                {
                    return (false, $"Empleado con ID {IdEmpleado} no encontrado."); // Indicar que el Empleado no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (false, $"Error al eliminar el Empleado: {ex.Message}");
            }
        }
        public (EmpleadoDTO Empleado, bool Success, string ErrorMessage) GetEmpleado(int IdEmpleado)
        {
            try
            {
                TblEmpleado empleado = db.TblEmpleados.Include(e => e.IdSucursalNavigation)
                                       .FirstOrDefault(x => x.IdEmpleado == IdEmpleado && x.Eliminado == false);


                EmpleadoDTO objEmpleadoDto = new();
                objEmpleadoDto.IdEmpleado = empleado.IdEmpleado;
                objEmpleadoDto.NombreEmpleado = empleado.NombreEmpleado;
                objEmpleadoDto.ApellidoPaterno = empleado.ApellidoPaterno;
                objEmpleadoDto.ApellidoMaterno = empleado.ApellidoMaterno;
                objEmpleadoDto.UsuarioSistema = empleado.UsuarioSistema;
                objEmpleadoDto.ContrasenaSistema = empleado.ContrasenaSistema;
                objEmpleadoDto.Nss = empleado.Nss;
                objEmpleadoDto.IdSucursal = empleado.IdSucursal;
                objEmpleadoDto.NombreSucursal = empleado.IdSucursalNavigation.NombreSucursal;
                objEmpleadoDto.TipoUsuario = empleado.TipoUsuario;

                

                if (empleado != null)
                {
                    return (objEmpleadoDto, true, ""); // Devolver el Empleado encontrado y éxito sin mensaje de error
                }
                else
                {
                    return (null, false, $"Empleado con ID {IdEmpleado} no encontrado o está eliminado."); // Indicar que el Empleado no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener Empleado: {ex.Message}");
            }
        }


        public (List<EmpleadoDTO> Empleados, bool Success, string ErrorMessage) GetEmpleadosPorTipo(string tipoUsuario)
        {
            try
            {
                List<TblEmpleado> lstEmpleado = db.TblEmpleados
                    .Include(e => e.IdSucursalNavigation)
                    .Where(x => x.TipoUsuario == tipoUsuario && x.Eliminado == false)
                    .ToList();

                if (lstEmpleado != null && lstEmpleado.Count > 0)
                {
                    List<EmpleadoDTO> lstEmpleadoDto = lstEmpleado.Select(objEmpleado => new EmpleadoDTO
                    {
                        IdEmpleado = objEmpleado.IdEmpleado,
                        NombreEmpleado = objEmpleado.NombreEmpleado,
                        ApellidoPaterno = objEmpleado.ApellidoPaterno,
                        ApellidoMaterno = objEmpleado.ApellidoMaterno,
                        UsuarioSistema = objEmpleado.UsuarioSistema,
                        ContrasenaSistema = objEmpleado.ContrasenaSistema,
                        Nss = objEmpleado.Nss,
                        IdSucursal = objEmpleado.IdSucursal,
                        NombreSucursal = objEmpleado.IdSucursalNavigation.NombreSucursal,
                        TipoUsuario = objEmpleado.TipoUsuario
                    }).ToList();

                    return (lstEmpleadoDto, true, ""); // Empleados encontrados y éxito sin mensaje de error
                }
                else
                {
                    return (new List<EmpleadoDTO>(), false, $"No se encontraron empleados con TipoUsuario: {tipoUsuario}.");
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción: registrarla, notificarla, etc.
                return (null, false, $"Error al obtener empleados por TipoUsuario: {ex.Message}");
            }
        }



        public (List<EmpleadoDTO> Empleados, bool Success, string ErrorMessage) GetEmpleados()
        {
            try
            {
                List<TblEmpleado> Empleados = db.TblEmpleados
                                              .Where(x => x.Eliminado == false)
                                              .Include(e => e.IdSucursalNavigation) // Incluir la propiedad de navegación IdEmpresaNavigation
                                              .ToList();


                List<EmpleadoDTO> lstEmpleadoDto = new List<EmpleadoDTO>();
                foreach (var empleado in Empleados)
                {
                    EmpleadoDTO objEmpleadoDto = new EmpleadoDTO();
                    objEmpleadoDto.IdEmpleado = empleado.IdEmpleado;
                    objEmpleadoDto.NombreEmpleado = empleado.NombreEmpleado;
                    objEmpleadoDto.ApellidoPaterno = empleado.ApellidoPaterno;
                    objEmpleadoDto.ApellidoMaterno = empleado.ApellidoMaterno;
                    objEmpleadoDto.UsuarioSistema = empleado.UsuarioSistema;
                    objEmpleadoDto.ContrasenaSistema = empleado.ContrasenaSistema;
                    objEmpleadoDto.Nss = empleado.Nss;
                    objEmpleadoDto.IdSucursal = empleado.IdSucursal;
                    objEmpleadoDto.NombreSucursal = empleado.IdSucursalNavigation.NombreSucursal;
                    objEmpleadoDto.TipoUsuario = empleado.TipoUsuario;

                    // Puedes asignar otros valores según sea necesario
                    lstEmpleadoDto.Add(objEmpleadoDto);
                }



                return (lstEmpleadoDto, true, ""); // Devolver la lista de Empleados y éxito sin mensaje de error
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener Empleados: {ex.Message}");
            }
        }
        public (bool Success, string ErrorMessage) GuardarEmpleado(TblEmpleado objEmpleado)
        {
            try
            {
                db.TblEmpleados.Add(objEmpleado);
                db.SaveChanges();
                return (true, ""); // Devolver éxito sin mensaje de error
            }
            catch (DbUpdateException ex)
            {
                // Capturar errores específicos de Entity Framework al guardar en la base de datos
                return (false, $"Error al guardar el Empleado: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                // Capturar cualquier otra excepción no controlada
                return (false, $"Error inesperado al guardar el Empleado: {ex.Message}");
            }
        }
    }
}
