using Datos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negocios.Interfaces;

namespace WebContratos.Controllers
{
    public class ProductoController : Controller
    {
        IProducto _Producto;
        public ProductoController(IProducto _Producto)
        {
            this._Producto = _Producto;
        }

        [HttpGet]
        [Route("Productos")]
        public IActionResult GetProductos()
        {
            var resultado = _Producto.GetProductos();

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de Productos
                return Ok(resultado.Productos); // Devolver un código 200 OK con la lista de Productos
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpGet]
        [Route("Producto/getProductoById")]
        public IActionResult GetProductoById(int IdProducto)
        {
            var resultado = _Producto.GetProducto(IdProducto);

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de Productos
                return Ok(resultado.Producto); // Devolver un código 200 OK con la lista de Productos
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpPost]
        [Route("Producto/guardar")]
        public IActionResult AgregarProducto([FromBody] TblProducto Producto)
        {
            try
            {
                // Validar la entrada recibida
                if (Producto == null)
                {
                    return BadRequest("La solicitud no contiene datos válidos para la Producto.");
                }

                // Intentar guardar la Producto
                var resultado = _Producto.GuardarProducto(Producto);

                if (resultado.Success)
                {
                    // Devolver un código 201 Created con la Producto creada
                    return CreatedAtAction(nameof(GetProductoById), new { id = Producto.IdProducto }, Producto);
                }
                else
                {
                    // Devolver un código 400 Bad Request con el mensaje de error
                    return BadRequest($"Error al guardar la Producto: {resultado.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Manejar y registrar cualquier excepción no controlada
                Console.WriteLine($"Error al procesar la solicitud: {ex.Message}");
                // Devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, "Se produjo un error interno al procesar la solicitud.");
            }
        }


        [HttpDelete]
        [Route("Productos/eliminar")]
        public IActionResult EliminarProducto(int IdProducto)
        {
            try
            {
                if (IdProducto == null || IdProducto <= 0)
                {
                    return BadRequest("Se requiere proporcionar una Producto válida para eliminar.");
                }

                var resultado = _Producto.EliminarProducto(IdProducto);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La Producto se eliminó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = "La Producto no pudo ser encontrada para eliminar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al eliminar la Producto: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al eliminar la Producto." });
            }
        }

        [HttpPut]
        [Route("Productos/actualizar")]
        public IActionResult ActualizarProducto([FromBody] TblProducto Producto)
        {
            try
            {
                if (Producto == null || Producto.IdProducto <= 0)
                {
                    return BadRequest("Se requiere proporcionar una Producto válida para actualizar.");
                }

                var resultado = _Producto.ActualizarProducto(Producto);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La Producto se actualizó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = "La Producto no pudo ser encontrada para actualizar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al actualizar la Producto: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al actualizar la Producto." });
            }
        }

        // Endpoint para la búsqueda de productos por nombre
        [HttpGet("Productos/buscar")]
        public async Task<IActionResult> BuscarProductos(string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                return BadRequest("El término de búsqueda no puede estar vacío");
            }

            var resultado = _Producto.BuscarProductos(term);           

           return Ok(resultado.Producto);
        }
    }
}
