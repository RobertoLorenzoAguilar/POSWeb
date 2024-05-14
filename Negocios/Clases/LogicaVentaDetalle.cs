using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Models;
using Microsoft.EntityFrameworkCore;
using Negocios.Interfaces;
namespace Negocios.Clases
{
    public class LogicaVentaDetalle : IVentaDetalle
    {
        private PosDbContext db;
        public LogicaVentaDetalle(PosDbContext db)
        {
            this.db = db;

        }
        public (bool Success, string ErrorMessage) ActualizarVentaDetalle(TblVentaDetalle updatedVentaDetalle)
        {
            try
            {
                // Buscar el VentaDetalle existente en la base de datos
                var existingVentaDetalle = db.TblVentaDetalles.Find(updatedVentaDetalle.IdVentaDetalle);

                if (existingVentaDetalle != null)
                {
                    // Se remueve la instancia del contexto para poder eliminarla
                    db.TblVentaDetalles.Remove(existingVentaDetalle);  
                    // Adjuntar el VentaDetalle actualizado al contexto de Entity Framework
                    db.TblVentaDetalles.Attach(updatedVentaDetalle);

                    // Marcar la entidad como modificada
                    db.Entry(updatedVentaDetalle).State = EntityState.Modified;

                    // Guardar los cambios en la base de datos
                    db.SaveChanges();

                    return (true, ""); // Operación exitosa
                }
                else
                {
                    return (false, $"VentaDetalle con ID {updatedVentaDetalle.IdVentaDetalle} no encontrado."); // VentaDetalle no encontrado
                }
            }
            catch (Exception ex)
            {
                // Capturar y manejar la excepción                
                return (false, $"Error al actualizar el VentaDetalle: {ex.Message}");
            }
        }
        public (bool Success, string ErrorMessage) EliminarVentaDetalle(int IdVentaDetalle)
        {
            try
            {
                var VentaDetalle = db.TblVentaDetalles.FirstOrDefault(x => x.IdVentaDetalle == IdVentaDetalle);

                if (VentaDetalle != null)
                {
                    VentaDetalle.Eliminado = true; // Marcamos el VentaDetalle como eliminado (eliminación lógica)
                    db.SaveChanges();
                    return (true, ""); // Devolver éxito sin mensaje de error
                }
                else
                {
                    return (false, $"VentaDetalle con ID {IdVentaDetalle} no encontrado."); // Indicar que el VentaDetalle no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (false, $"Error al eliminar el VentaDetalle: {ex.Message}");
            }
        }
        public (TblVentaDetalle VentaDetalle, bool Success, string ErrorMessage) GetVentaDetalle(int IdVentaDetalle)
        {
            try
            {
                TblVentaDetalle VentaDetalle = db.TblVentaDetalles
                                       .FirstOrDefault(x => x.IdVentaDetalle == IdVentaDetalle && x.Eliminado == false);

                if (VentaDetalle != null)
                {
                    return (VentaDetalle, true, ""); // Devolver el VentaDetalle encontrado y éxito sin mensaje de error
                }
                else
                {
                    return (null, false, $"VentaDetalle con ID {IdVentaDetalle} no encontrado o está eliminado."); // Indicar que el VentaDetalle no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener VentaDetalle: {ex.Message}");
            }
        }
        public (List<TblVentaDetalle> VentaDetalles, bool Success, string ErrorMessage) GetVentaDetalles()
        {
            try
            {
                List<TblVentaDetalle> VentaDetalles = db.TblVentaDetalles
                                              .Where(x => x.Eliminado == false)
                                              .ToList();

                return (VentaDetalles, true, ""); // Devolver la lista de VentaDetalles y éxito sin mensaje de error
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener VentaDetalles: {ex.Message}");
            }
        }
        public (bool Success, string ErrorMessage) GuardarVentaDetalle(TblVentaDetalle objVentaDetalle)
        {
            try
            {
                db.TblVentaDetalles.Add(objVentaDetalle);
                db.SaveChanges();
                return (true, ""); // Devolver éxito sin mensaje de error
            }
            catch (DbUpdateException ex)
            {
                // Capturar errores específicos de Entity Framework al guardar en la base de datos
                return (false, $"Error al guardar el VentaDetalle: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                // Capturar cualquier otra excepción no controlada
                return (false, $"Error inesperado al guardar el VentaDetalle: {ex.Message}");
            }
        }
    }
}
