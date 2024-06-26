﻿using Datos.Models;
using Microsoft.EntityFrameworkCore;
using Negocios.DTO;
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
        public (bool Success, string ErrorMessage) ActualizarVenta(VentaDTO updatedVenta)
        {
            try
            {
                // Buscar la Venta existente en la base de datos
                var existingVenta = db.TblVenta
                        .FirstOrDefault(x => x.IdVenta == updatedVenta.IdVenta && x.Eliminado == false);

                if (existingVenta != null)
                {
                    // Actualizar las propiedades de la entidad existente
                    existingVenta.TipoVenta = updatedVenta.TipoVenta;
                    existingVenta.IdCliente = updatedVenta.IdCliente;
                    existingVenta.IdEmpleado = updatedVenta.IdEmpleado;
                    //existingVenta.FechaVenta = updatedVenta.FechaVenta;
                    existingVenta.TotalVenta = updatedVenta.TotalVenta;

                    // Eliminar los detalles de la venta existentes
                    var lstDetallesVenta = db.TblVentaDetalles
                        .Where(x => x.IdVenta == updatedVenta.IdVenta && x.Eliminado == false).ToList();

                    db.TblVentaDetalles.RemoveRange(lstDetallesVenta);

                    // Guardar los nuevos detalles
                    foreach (var objCantIDProd in updatedVenta.LstProducto)
                    {
                        TblVentaDetalle objDetalle = new TblVentaDetalle();
                        objDetalle.IdVenta = updatedVenta.IdVenta;
                        objDetalle.Cantidad = objCantIDProd.cantidad;
                        objDetalle.IdProducto = objCantIDProd.idProducto;
                        db.TblVentaDetalles.Add(objDetalle);
                    }

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
        public (VentaDTO Venta, bool Success, string ErrorMessage) GetVenta(int IdVenta)
        {
            try
            {
                TblVentum objVenta = db.TblVenta
                        .Include(e => e.TblVentaDetalles)  // se trae el detalle de venta ligado por el id 
                        .Include(e => e.IdClienteNavigation)
                        .Include(e => e.IdEmpleadoNavigation)
                        .Include(e => e.IdEmpleadoNavigation.IdSucursalNavigation)
                        .FirstOrDefault(x => x.IdVenta == IdVenta && x.Eliminado == false);


                VentaDTO objVentaDTO = new VentaDTO();
                objVentaDTO.IdVenta = objVenta.IdVenta;
                objVentaDTO.TipoVenta = objVenta.TipoVenta;
                objVentaDTO.IdCliente = objVenta.IdCliente;
                objVentaDTO.RfcCliente = objVenta.IdClienteNavigation.RfcCliente;
                objVentaDTO.IdEmpleado = objVenta.IdEmpleado;
                objVentaDTO.UsuarioEmpleado = objVenta.IdEmpleadoNavigation.NombreEmpleado;
                objVentaDTO.IdSucursal = objVenta.IdEmpleadoNavigation.IdSucursal;
                objVentaDTO.NombreSucursal = objVenta.IdEmpleadoNavigation.IdSucursalNavigation.NombreSucursal;
                objVentaDTO.FechaVenta = objVenta.FechaVenta;
                objVentaDTO.TotalVenta = objVenta.TotalVenta;

                foreach (var objProducto in objVenta.TblVentaDetalles)

                {
                    ProductoCantidad lstProductoCantidad = new();
                    lstProductoCantidad.idProducto = objProducto.IdProducto;
                    lstProductoCantidad.cantidad = objProducto.Cantidad;
                    objVentaDTO.LstProducto.Add(lstProductoCantidad);
                }

                if (objVentaDTO != null)
                {
                    return (objVentaDTO, true, ""); // Devolver el Venta encontrado y éxito sin mensaje de error
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
        //public (List<TblVentum> Ventas, bool Success, string ErrorMessage) GetVentas()
        //{
        //    try
        //    {
        //        List<TblVentum> Ventas = db.TblVenta
        //                                      .Where(x => x.Eliminado == false)
        //                                      .ToList();

        //        return (Ventas, true, ""); // Devolver la lista de Ventas y éxito sin mensaje de error
        //    }
        //    catch (Exception ex)
        //    {
        //        // Devolver una tupla indicando fallo y el mensaje de error
        //        return (null, false, $"Error al obtener Ventas: {ex.Message}");
        //    }
        //}

        public (List<VentaDTO> Ventas, bool Success, string ErrorMessage) GetVentas()
        {
            try
            {
                List<TblVentum> lstVentas = db.TblVenta
                .Include(e => e.IdClienteNavigation)
                .Include(e => e.IdEmpleadoNavigation)
                .Include(e => e.IdEmpleadoNavigation.IdSucursalNavigation)
                                              .Where(x => x.Eliminado == false)
                                              .ToList();


                List<VentaDTO> lstVentaDTO = new List<VentaDTO>();
                foreach (var objVenta in lstVentas)
                {
                    VentaDTO objVentaDTO = new VentaDTO();
                    objVentaDTO.IdVenta = objVenta.IdVenta;
                    objVentaDTO.TipoVenta = objVenta.TipoVenta;
                    objVentaDTO.IdCliente = objVenta.IdCliente;
                    objVentaDTO.RfcCliente = objVenta.IdClienteNavigation.RfcCliente;
                    objVentaDTO.IdEmpleado = objVenta.IdEmpleado;
                    objVentaDTO.UsuarioEmpleado = objVenta.IdEmpleadoNavigation.NombreEmpleado;
                    objVentaDTO.IdSucursal = objVenta.IdEmpleadoNavigation.IdSucursal;
                    objVentaDTO.NombreSucursal = objVenta.IdEmpleadoNavigation.IdSucursalNavigation.NombreSucursal;
                    objVentaDTO.FechaVenta = objVenta.FechaVenta;
                    objVentaDTO.TotalVenta = objVenta.TotalVenta;
                    // Puedes asignar otros valores según sea necesario
                    lstVentaDTO.Add(objVentaDTO);
                }

                return (lstVentaDTO, true, ""); // Devolver la lista de Ventas y éxito sin mensaje de error
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener Ventas: {ex.Message}");
            }
        }

        public (bool Success, string ErrorMessage) GuardarVenta(VentaDTO objVentaDTO)
        {
            try
            {

                // Setear la hora actual
                DateTime currentDate = DateTime.Now;
                //  Guardamos primero la venta y recuperamos el ig
                TblVentum objTblVenta = new TblVentum();
                objTblVenta.IdVenta = objVentaDTO.IdVenta;
                objTblVenta.TipoVenta = objVentaDTO.TipoVenta;
                objTblVenta.IdCliente = objVentaDTO.IdCliente;
                objTblVenta.IdEmpleado = objVentaDTO.IdEmpleado;
                objTblVenta.FechaVenta = currentDate;
                objTblVenta.TotalVenta = objVentaDTO.TotalVenta;
                db.TblVenta.Add(objTblVenta);

                db.SaveChanges(); //    guardamos a venta

                int IdVenta = objTblVenta.IdVenta;

                foreach (var objCantIDProd in objVentaDTO.LstProducto)
                {
                    TblVentaDetalle objDetalle = new TblVentaDetalle();
                    objDetalle.IdVenta = IdVenta;
                    objDetalle.Cantidad = objCantIDProd.cantidad;
                    objDetalle.IdProducto = objCantIDProd.idProducto;
                    db.TblVentaDetalles.Add(objDetalle);
                    db.SaveChanges(); //    guardamos a venta
                }

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
