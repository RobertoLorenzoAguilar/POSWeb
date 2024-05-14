﻿using Datos.Models;
using Microsoft.EntityFrameworkCore;
using Negocios.Interfaces;
namespace Negocios.Clases
{
    public class LogicaVenta : IVenta
    {
        private PosDbContext db;
        public LogicaVenta(PosDbContext db)
        {
            this.db = db;

        }
        public (bool Success, string ErrorMessage) ActualizarVenta(TblVentum updatedVenta)
        {
            try
            {
                // Buscar el Venta existente en la base de datos
                var existingVenta = db.TblVenta.Find(updatedVenta.IdVenta);

                if (existingVenta != null)
                {
                    // Se remueve la instancia del contexto para poder eliminarla
                    db.TblVenta.Remove(existingVenta);  
                    // Adjuntar el Venta actualizado al contexto de Entity Framework
                    db.TblVenta.Attach(updatedVenta);

                    // Marcar la entidad como modificada
                    db.Entry(updatedVenta).State = EntityState.Modified;

                    // Guardar los cambios en la base de datos
                    db.SaveChanges();

                    return (true, ""); // Operación exitosa
                }
                else
                {
                    return (false, $"Venta con ID {updatedVenta.IdVenta} no encontrado."); // Venta no encontrado
                }
            }
            catch (Exception ex)
            {
                // Capturar y manejar la excepción                
                return (false, $"Error al actualizar el Venta: {ex.Message}");
            }
        }
        public (bool Success, string ErrorMessage) EliminarVenta(int IdVenta)
        {
            try
            {
                var Venta = db.TblVenta.FirstOrDefault(x => x.IdVenta == IdVenta);

                if (Venta != null)
                {
                    Venta.Eliminado = true; // Marcamos el Venta como eliminado (eliminación lógica)
                    db.SaveChanges();
                    return (true, ""); // Devolver éxito sin mensaje de error
                }
                else
                {
                    return (false, $"Venta con ID {IdVenta} no encontrado."); // Indicar que el Venta no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (false, $"Error al eliminar el Venta: {ex.Message}");
            }
        }
        public (TblVentum Venta, bool Success, string ErrorMessage) GetVenta(int IdVenta)
        {
            try
            {
                TblVentum Venta = db.TblVenta
                                       .FirstOrDefault(x => x.IdVenta == IdVenta && x.Eliminado == false);

                if (Venta != null)
                {
                    return (Venta, true, ""); // Devolver el Venta encontrado y éxito sin mensaje de error
                }
                else
                {
                    return (null, false, $"Venta con ID {IdVenta} no encontrado o está eliminado."); // Indicar que el Venta no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener Venta: {ex.Message}");
            }
        }
        public (List<TblVentum> Ventas, bool Success, string ErrorMessage) GetVentas()
        {
            try
            {
                List<TblVentum> Ventas = db.TblVenta
                                              .Where(x => x.Eliminado == false)
                                              .ToList();

                return (Ventas, true, ""); // Devolver la lista de Ventas y éxito sin mensaje de error
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener Ventas: {ex.Message}");
            }
        }
        public (bool Success, string ErrorMessage) GuardarVenta(TblVentum objVenta)
        {
            try
            {
                db.TblVenta.Add(objVenta);
                db.SaveChanges();
                return (true, ""); // Devolver éxito sin mensaje de error
            }
            catch (DbUpdateException ex)
            {
                // Capturar errores específicos de Entity Framework al guardar en la base de datos
                return (false, $"Error al guardar el Venta: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                // Capturar cualquier otra excepción no controlada
                return (false, $"Error inesperado al guardar el Venta: {ex.Message}");
            }
        }
    }
}
