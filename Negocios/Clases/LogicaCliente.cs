using Datos.Models;
using Microsoft.EntityFrameworkCore;
using Negocios.DTO;
using Negocios.Interfaces;
namespace Negocios.Clases
{
    public class LogicaCliente : ICliente
    {
        private PosDbContext db;
        public LogicaCliente(PosDbContext db)
        {
            this.db = db;

        }
        public (bool Success, string ErrorMessage) ActualizarCliente(TblCliente updatedCliente)
        {
            try
            {
                // Buscar el Cliente existente en la base de datos
                var existingCliente = db.TblClientes.Find(updatedCliente.IdCliente);

                if (existingCliente != null)
                {
                    // Se remueve la instancia del contexto para poder eliminarla
                    db.TblClientes.Remove(existingCliente);  
                    // Adjuntar el Cliente actualizado al contexto de Entity Framework
                    db.TblClientes.Attach(updatedCliente);

                    // Marcar la entidad como modificada
                    db.Entry(updatedCliente).State = EntityState.Modified;

                    // Guardar los cambios en la base de datos
                    db.SaveChanges();

                    return (true, ""); // Operación exitosa
                }
                else
                {
                    return (false, $"Cliente con ID {updatedCliente.IdCliente} no encontrado."); // Cliente no encontrado
                }
            }
            catch (Exception ex)
            {
                // Capturar y manejar la excepción                
                return (false, $"Error al actualizar el Cliente: {ex.Message}");
            }
        }
        public (bool Success, string ErrorMessage) EliminarCliente(int IdCliente)
        {
            try
            {
                var Cliente = db.TblClientes.FirstOrDefault(x => x.IdCliente == IdCliente);

                if (Cliente != null)
                {
                    Cliente.Eliminado = true; // Marcamos el Cliente como eliminado (eliminación lógica)
                    db.SaveChanges();
                    return (true, ""); // Devolver éxito sin mensaje de error
                }
                else
                {
                    return (false, $"Cliente con ID {IdCliente} no encontrado."); // Indicar que el Cliente no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (false, $"Error al eliminar el Cliente: {ex.Message}");
            }
        }
        public (ClienteDTO Cliente, bool Success, string ErrorMessage) GetCliente(int IdCliente)
        {
            try
            {
                TblCliente Cliente = db.TblClientes.Include(e => e.IdEmpresaNavigation)
                                       .FirstOrDefault(x => x.IdCliente == IdCliente && x.Eliminado == false);


                ClienteDTO objClienteDTO = new ClienteDTO();
                objClienteDTO.IdCliente = Cliente.IdCliente;
                objClienteDTO.ApellidoPaterno = Cliente.ApellidoPaterno;
                objClienteDTO.ApellidoMaterno = Cliente.ApellidoMaterno;
                objClienteDTO.NombreCliente = Cliente.NombreCliente;
                objClienteDTO.RfcCliente = Cliente.RfcCliente;
                objClienteDTO.CalleDireccionCliente = Cliente.CalleDireccionCliente;
                objClienteDTO.NumeroDirecionCiente = Cliente.NumeroDirecionCiente;
                objClienteDTO.IdEmpresa = Cliente.IdEmpresa;
                objClienteDTO.TipoCliente = Cliente.TipoCliente;
                objClienteDTO.Descuento = Cliente.Descuento;
                objClienteDTO.IdEmpleado = Cliente.IdEmpleado;  // Cuando es diferente público en general se ele asigna un asesor
                if (objClienteDTO.IdEmpleado == 0)
                {
                    objClienteDTO.TipoUsuario = "N/A";
                }
                else
                {
                    objClienteDTO.TipoUsuario = db.TblEmpleados.Where(e => e.IdEmpleado == Cliente.IdEmpleado).Select(e => e.NombreEmpleado + " " + e.ApellidoPaterno).FirstOrDefault();
                }

                
                objClienteDTO.RazonSocialEmpresa = Cliente.IdEmpresaNavigation.RazonSocialEmpresa;


                if (Cliente != null)
                {
                    return (objClienteDTO, true, ""); // Devolver el Cliente encontrado y éxito sin mensaje de error
                }
                else
                {
                    return (null, false, $"Cliente con ID {IdCliente} no encontrado o está eliminado."); // Indicar que el Cliente no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener Cliente: {ex.Message}");
            }
        }
        public (List<ClienteDTO> Clientes, bool Success, string ErrorMessage) GetClientes()
        {
            try
            {
                List<TblCliente> Clientes = db.TblClientes
                                              .Where(x => x.Eliminado == false)
                                              .Include(e=> e.IdEmpresaNavigation)
                                              .ToList();

                List<ClienteDTO> lstClienteDTO = new List<ClienteDTO>();
                foreach (var cliente in Clientes)
                {
                    ClienteDTO objClienteDTO = new ClienteDTO();
                    objClienteDTO.IdCliente = cliente.IdCliente;
                    objClienteDTO.ApellidoPaterno = cliente.ApellidoPaterno;
                    objClienteDTO.ApellidoMaterno = cliente.ApellidoMaterno;
                    objClienteDTO.NombreCliente = cliente.NombreCliente;
                    objClienteDTO.RfcCliente = cliente.RfcCliente;
                    objClienteDTO.CalleDireccionCliente = cliente.CalleDireccionCliente;
                    objClienteDTO.NumeroDirecionCiente = cliente.NumeroDirecionCiente;
                    objClienteDTO.IdEmpresa = cliente.IdEmpresa;
                    objClienteDTO.TipoCliente = cliente.TipoCliente;
                    objClienteDTO.Descuento = cliente.Descuento;
                    objClienteDTO.RazonSocialEmpresa = cliente.IdEmpresaNavigation.RazonSocialEmpresa;
                    objClienteDTO.IdEmpleado = cliente.IdEmpleado;  // Cuando es diferente público en general se ele asigna un asesor
                    if (objClienteDTO.IdEmpleado ==0)
                    {
                        objClienteDTO.TipoUsuario = "N/A";
                    }
                    else
                    {
                        objClienteDTO.TipoUsuario = db.TblEmpleados.Where(e => e.IdEmpleado == cliente.IdEmpleado).Select(e => e.NombreEmpleado + " " + e.ApellidoPaterno).FirstOrDefault();
                    }
                    
                    lstClienteDTO.Add(objClienteDTO);
                }

                return (lstClienteDTO, true, ""); // Devolver la lista de Clientes y éxito sin mensaje de error
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener Clientes: {ex.Message}");
            }
        }
        public (bool Success, string ErrorMessage) GuardarCliente(TblCliente objCliente)
        {
            try
            {
                db.TblClientes.Add(objCliente);
                db.SaveChanges();
                return (true, ""); // Devolver éxito sin mensaje de error
            }
            catch (DbUpdateException ex)
            {
                // Capturar errores específicos de Entity Framework al guardar en la base de datos
                return (false, $"Error al guardar el Cliente: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                // Capturar cualquier otra excepción no controlada
                return (false, $"Error inesperado al guardar el Cliente: {ex.Message}");
            }
        }
    }
}
