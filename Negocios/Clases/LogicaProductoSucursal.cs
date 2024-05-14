using Datos.Models;
using Microsoft.EntityFrameworkCore;
using Negocios.DTO;
using Negocios.Interfaces;
namespace Negocios.Clases
{
    public class LogicaProductoSucursal : IProductoSucursal
    {
        private PosDbContext db;
        public LogicaProductoSucursal(PosDbContext db)
        {
            this.db = db;

        }
        public (bool Success, string ErrorMessage) ActualizarProductoSucursal(TblProductoSucursal updatedProductoSucursal)
        {
            try
            {
                // Buscar el ProductoSucursal existente en la base de datos
                var existingProductoSucursal = db.TblProductoSucursals.Find(updatedProductoSucursal.IdProducto);

                if (existingProductoSucursal != null)
                {
                    // Se remueve la instancia del contexto para poder eliminarla
                    db.TblProductoSucursals.Remove(existingProductoSucursal);  
                    // Adjuntar el ProductoSucursal actualizado al contexto de Entity Framework
                    db.TblProductoSucursals.Attach(updatedProductoSucursal);

                    // Marcar la entidad como modificada
                    db.Entry(updatedProductoSucursal).State = EntityState.Modified;

                    // Guardar los cambios en la base de datos
                    db.SaveChanges();

                    return (true, ""); // Operación exitosa
                }
                else
                {
                    return (false, $"ProductoSucursal con ID {updatedProductoSucursal.IdProducto} no encontrado."); // ProductoSucursal no encontrado
                }
            }
            catch (Exception ex)
            {
                // Capturar y manejar la excepción                
                return (false, $"Error al actualizar el ProductoSucursal: {ex.Message}");
            }
        }




        public (bool Success, string ErrorMessage) EliminarProductoSucursal(int IdProductoSucursal)
        {
            try
            {
                var ProductoSucursal = db.TblProductoSucursals.FirstOrDefault(x => x.IdProducto == IdProductoSucursal);

                if (ProductoSucursal != null)
                {
                    ProductoSucursal.Eliminado = true; // Marcamos el ProductoSucursal como eliminado (eliminación lógica)
                    db.SaveChanges();
                    return (true, ""); // Devolver éxito sin mensaje de error
                }
                else
                {
                    return (false, $"ProductoSucursal con ID {IdProductoSucursal} no encontrado."); // Indicar que el ProductoSucursal no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (false, $"Error al eliminar el ProductoSucursal: {ex.Message}");
            }
        }
        public (ProductoSucursalDTO ProductoSucursal, bool Success, string ErrorMessage) GetProductoSucursal(int IdProductoSucursal)
        {
            try
            {
                TblProductoSucursal ProductoSucursal = db.TblProductoSucursals.Include(e => e.IdSucursalNavigation)
                                       .FirstOrDefault(x => x.IdProducto == IdProductoSucursal && x.Eliminado == false);


                ProductoSucursalDTO objProductoSucursalDto = new ProductoSucursalDTO();
                objProductoSucursalDto.IdProducto = ProductoSucursal.IdProducto;
                objProductoSucursalDto.NombreProducto = ProductoSucursal.IdProductoNavigation.NombreProducto;
                objProductoSucursalDto.IdSucursal = ProductoSucursal.IdSucursal;
                objProductoSucursalDto.NombreSucursal = ProductoSucursal.IdSucursalNavigation.NombreSucursal;
                objProductoSucursalDto.Precio = ProductoSucursal.Precio;
                              

                if (ProductoSucursal != null)
                {
                    return (objProductoSucursalDto, true, ""); // Devolver el ProductoSucursal encontrado y éxito sin mensaje de error
                }
                else
                {
                    return (null, false, $"ProductoSucursal con ID {IdProductoSucursal} no encontrado o está eliminado."); // Indicar que el ProductoSucursal no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener ProductoSucursal: {ex.Message}");
            }
        }


        //public (List<ProductoSucursalDTO> ProductoSucursals, bool Success, string ErrorMessage) GetProductoSucursalsPorTipo(string tipoUsuario)
        //{
        //    try
        //    {
        //        List<TblProductoSucursal> lstProductoSucursal = db.TblProductoSucursals                    
        //            .Where(x => x.Eliminado == false)
        //            .ToList();

        //        if (lstProductoSucursal != null && lstProductoSucursal.Count > 0)
        //        {
        //            List<ProductoSucursalDTO> lstProductoSucursalDto = lstProductoSucursal.Select(objProductoSucursal => new ProductoSucursalDTO
        //            {
        //                IdProducto = objProductoSucursal.IdProducto,
        //                NombreProducto = objProductoSucursal.IdProductoNavigation.NombreProducto,
        //                IdSucursal = objProductoSucursal.IdSucursal,
        //                NombreSucursal = objProductoSucursal.IdSucursalNavigation.NombreSucursal,
        //                Precio = objProductoSucursal.Precio
        //            }).ToList();

        //            return (lstProductoSucursalDto, true, ""); // ProductoSucursals encontrados y éxito sin mensaje de error
        //        }
        //        else
        //        {
        //            return (new List<ProductoSucursalDTO>(), false, $"No se encontraron ProductoSucursals con TipoUsuario: {tipoUsuario}.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar la excepción: registrarla, notificarla, etc.
        //        return (null, false, $"Error al obtener ProductoSucursals por TipoUsuario: {ex.Message}");
        //    }
        //}



        public (List<ProductoSucursalDTO> ProductoSucursals, bool Success, string ErrorMessage) GetProductoSucursals()
        {
            try
            {
                List<TblProductoSucursal> ProductoSucursals = db.TblProductoSucursals
                                              .Where(x => x.Eliminado == false)
                                              .Include(e => e.IdSucursalNavigation) // Incluir la propiedad de navegación IdEmpresaNavigation
                                              .ToList();


                List<ProductoSucursalDTO> lstProductoSucursalDto = new List<ProductoSucursalDTO>();
                foreach (var ProductoSucursal in ProductoSucursals)
                {
                    ProductoSucursalDTO objProductoSucursalDto = new ProductoSucursalDTO();
                    objProductoSucursalDto.IdProducto = ProductoSucursal.IdProducto;
                    objProductoSucursalDto.NombreProducto = ProductoSucursal.IdProductoNavigation.NombreProducto;
                    objProductoSucursalDto.IdSucursal = ProductoSucursal.IdSucursal;
                    objProductoSucursalDto.NombreSucursal = ProductoSucursal.IdSucursalNavigation.NombreSucursal;
                    objProductoSucursalDto.Precio = ProductoSucursal.Precio;


                    // Puedes asignar otros valores según sea necesario
                    lstProductoSucursalDto.Add(objProductoSucursalDto);
                }



                return (lstProductoSucursalDto, true, ""); // Devolver la lista de ProductoSucursals y éxito sin mensaje de error
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener ProductoSucursals: {ex.Message}");
            }
        }
        public (bool Success, string ErrorMessage) GuardarProductoSucursal(TblProductoSucursal objProductoSucursal)
        {
            try
            {
                db.TblProductoSucursals.Add(objProductoSucursal);
                db.SaveChanges();
                return (true, ""); // Devolver éxito sin mensaje de error
            }
            catch (DbUpdateException ex)
            {
                // Capturar errores específicos de Entity Framework al guardar en la base de datos
                return (false, $"Error al guardar el ProductoSucursal: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                // Capturar cualquier otra excepción no controlada
                return (false, $"Error inesperado al guardar el ProductoSucursal: {ex.Message}");
            }
        }
    }
}
