using Datos.Models;
using Microsoft.AspNetCore.Mvc;
using Negocios.Interfaces;

namespace WebContratos.Controllers
{
    public class ProductoSucursalController : Controller
    {
        IProductoSucursal _ProductoSucursal;
        public ProductoSucursalController(IProductoSucursal _ProductoSucursal)
        {
            this._ProductoSucursal = _ProductoSucursal;
        }

        [HttpGet]
        [Route("ProductoSucursal")]
        public IActionResult GetProductoSucursal()
        {
            var resultado = _ProductoSucursal.GetProductoSucursals();

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de ProductoSucursal
                return Ok(resultado.ProductoSucursals); // Devolver un código 200 OK con la lista de ProductoSucursal
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

      
        [HttpGet]
        [Route("ProductoSucursal/getProductoSucursalById")]
        public IActionResult GetProductoSucursalById(int IdProductoSucursal)
        {
            var resultado = _ProductoSucursal.GetProductoSucursal(IdProductoSucursal);

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de ProductoSucursal
                return Ok(resultado.ProductoSucursal); // Devolver un código 200 OK con la lista de ProductoSucursal
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpPost]
        [Route("ProductoSucursal/guardar")]
        public IActionResult AgregarProductoSucursal([FromBody] TblProductoSucursal ProductoSucursal)
        {
            try
            {
                // Validar la entrada recibida
                if (ProductoSucursal == null)
                {
                    return BadRequest("La solicitud no contiene datos válidos para la ProductoSucursal.");
                }

                // Intentar guardar la ProductoSucursal
                var resultado = _ProductoSucursal.GuardarProductoSucursal(ProductoSucursal);

                if (resultado.Success)
                {
                    // Devolver un código 201 Created con la ProductoSucursal creada
                    return CreatedAtAction(nameof(GetProductoSucursalById), new { id = ProductoSucursal.IdProducto }, ProductoSucursal);
                }
                else
                {
                    // Devolver un código 400 Bad Request con el mensaje de error
                    return BadRequest($"Error al guardar la ProductoSucursal: {resultado.ErrorMessage}");
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
        [Route("ProductoSucursal/eliminar")]
        public IActionResult EliminarProductoSucursal(int IdProductoSucursal)
        {
            try
            {
                if (IdProductoSucursal == null || IdProductoSucursal <= 0)
                {
                    return BadRequest("Se requiere proporcionar una ProductoSucursal válida para eliminar.");
                }

                var resultado = _ProductoSucursal.EliminarProductoSucursal(IdProductoSucursal);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La ProductoSucursal se eliminó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = "La ProductoSucursal no pudo ser encontrada para eliminar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al eliminar la ProductoSucursal: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al eliminar la ProductoSucursal." });
            }
        }

        [HttpPut]
        [Route("ProductoSucursal/actualizar")]
        public IActionResult ActualizarProductoSucursal([FromBody] TblProductoSucursal ProductoSucursal)
        {
            try
            {
                if (ProductoSucursal == null || ProductoSucursal.IdProducto <= 0)
                {
                    return BadRequest("Se requiere proporcionar una ProductoSucursal válida para actualizar.");
                }

                var resultado = _ProductoSucursal.ActualizarProductoSucursal(ProductoSucursal);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La ProductoSucursal se actualizó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = "La ProductoSucursal no pudo ser encontrada para actualizar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al actualizar la ProductoSucursal: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al actualizar la ProductoSucursal." });
            }
        }
    }
}
