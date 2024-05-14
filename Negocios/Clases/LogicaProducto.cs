using Datos.Models;
using Microsoft.EntityFrameworkCore;
using Negocios.Interfaces;
namespace Negocios.Clases
{
    public class LogicaProducto : IProducto
    {
        private PosDbContext db;
        public LogicaProducto(PosDbContext db)
        {
            this.db = db;

        }
        public (bool Success, string ErrorMessage) ActualizarProducto(TblProducto updatedProducto)
        {
            try
            {
                // Buscar el Producto existente en la base de datos
                var existingProducto = db.TblProductos.Find(updatedProducto.IdProducto);

                if (existingProducto != null)
                {
                    // Se remueve la instancia del contexto para poder eliminarla
                    db.TblProductos.Remove(existingProducto);
                    // Adjuntar el Producto actualizado al contexto de Entity Framework
                    db.TblProductos.Attach(updatedProducto);

                    // Marcar la entidad como modificada
                    db.Entry(updatedProducto).State = EntityState.Modified;

                    // Guardar los cambios en la base de datos
                    db.SaveChanges();

                    return (true, ""); // Operación exitosa
                }
                else
                {
                    return (false, $"Producto con ID {updatedProducto.IdProducto} no encontrado."); // Producto no encontrado
                }
            }
            catch (Exception ex)
            {
                // Capturar y manejar la excepción                
                return (false, $"Error al actualizar el Producto: {ex.Message}");
            }
        }

        public (List<TblProducto> Producto, bool Success, string ErrorMessage) BuscarProductos(string term)
        {
            try
            {
                List<TblProducto> lstProducto = db.TblProductos
                                       .Where(p => EF.Functions.Like(p.NombreProducto, $"%{term}%") && p.Eliminado==false)
                                        //.Select(p => new { Id = p.IdProducto, Nombre = p.NombreProducto })
                                       .ToList();
                            
                            

                if (lstProducto != null)
                {
                    return (lstProducto, true, ""); // Devolver el Producto encontrado y éxito sin mensaje de error
                }
                else
                {
                    return (null, false, $"Producto con ID  no encontrado o está eliminado."); // Indicar que el Producto no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener Producto: {ex.Message}");
            }
        }

        public (bool Success, string ErrorMessage) EliminarProducto(int IdProducto)
        {
            try
            {
                var Producto = db.TblProductos.FirstOrDefault(x => x.IdProducto == IdProducto);

                if (Producto != null)
                {
                    Producto.Eliminado = true; // Marcamos el Producto como eliminado (eliminación lógica)
                    db.SaveChanges();
                    return (true, ""); // Devolver éxito sin mensaje de error
                }
                else
                {
                    return (false, $"Producto con ID {IdProducto} no encontrado."); // Indicar que el Producto no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (false, $"Error al eliminar el Producto: {ex.Message}");
            }
        }
        public (TblProducto Producto, bool Success, string ErrorMessage) GetProducto(int IdProducto)
        {
            try
            {
                TblProducto Producto = db.TblProductos
                                       .FirstOrDefault(x => x.IdProducto == IdProducto && x.Eliminado == false);

                if (Producto != null)
                {
                    return (Producto, true, ""); // Devolver el Producto encontrado y éxito sin mensaje de error
                }
                else
                {
                    return (null, false, $"Producto con ID {IdProducto} no encontrado o está eliminado."); // Indicar que el Producto no fue encontrado
                }
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener Producto: {ex.Message}");
            }
        }
        public (List<TblProducto> Productos, bool Success, string ErrorMessage) GetProductos()
        {
            try
            {
                List<TblProducto> Productos = db.TblProductos
                                              .Where(x => x.Eliminado == false)
                                              .ToList();

                return (Productos, true, ""); // Devolver la lista de Productos y éxito sin mensaje de error
            }
            catch (Exception ex)
            {
                // Devolver una tupla indicando fallo y el mensaje de error
                return (null, false, $"Error al obtener Productos: {ex.Message}");
            }
        }
        public (bool Success, string ErrorMessage) GuardarProducto(TblProducto objProducto)
        {
            try
            {
                db.TblProductos.Add(objProducto);
                db.SaveChanges();
                return (true, ""); // Devolver éxito sin mensaje de error
            }
            catch (DbUpdateException ex)
            {
                // Capturar errores específicos de Entity Framework al guardar en la base de datos
                return (false, $"Error al guardar el Producto: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                // Capturar cualquier otra excepción no controlada
                return (false, $"Error inesperado al guardar el Producto: {ex.Message}");
            }
        }
    }
}
