using Datos.Models;
using Microsoft.AspNetCore.Mvc;
using Negocios.DTO;
using Negocios.Interfaces;
using Newtonsoft.Json;
namespace WebContratos.Controllers
{
    public class VentaController : Controller
    {
        IVenta _Venta;
        public VentaController(IVenta _Venta)
        {
            this._Venta = _Venta;
        }

        [HttpGet]
        [Route("Ventas")]
        public IActionResult GetVentas()
        {
            var resultado = _Venta.GetVentas();

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de Ventas
                return Ok(resultado.Ventas); // Devolver un código 200 OK con la lista de Ventas
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpGet]
        [Route("Venta/getVentaById")]
        public IActionResult GetVentaById(int IdVenta)
        {
            var resultado = _Venta.GetVenta(IdVenta);

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de Ventas
                return Ok(resultado.Venta); // Devolver un código 200 OK con la lista de Ventas
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpPost]
        [Route("Venta/guardar")]
        public IActionResult AgregarVenta([FromBody] VentaDTO Venta)
        {
            try
            {
                // Validar la entrada recibida
                if (Venta == null)
                {
                    return BadRequest("La solicitud no contiene datos válidos para la Venta.");
                }       

                // Intentar guardar la Venta
                var resultado = _Venta.GuardarVenta(Venta);

                if (resultado.Success)
                {
                    // Devolver un código 201 Created con la Venta creada
                    return CreatedAtAction(nameof(GetVentaById), new { id = Venta.IdVenta }, Venta);
                }
                else
                {
                    // Devolver un código 400 Bad Request con el mensaje de error
                    return BadRequest($"Error al guardar la Venta: {resultado.ErrorMessage}");
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
        [Route("Ventas/eliminar")]
        public IActionResult EliminarVenta(int IdVenta)
        {
            try
            {
                if (IdVenta == null || IdVenta <= 0)
                {
                    return BadRequest("Se requiere proporcionar una Venta válida para eliminar.");
                }

                var resultado = _Venta.EliminarVenta(IdVenta);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La Venta se eliminó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = "La Venta no pudo ser encontrada para eliminar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al eliminar la Venta: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al eliminar la Venta." });
            }
        }

        [HttpPut]
        [Route("Ventas/actualizar")]
        public IActionResult ActualizarVenta([FromBody] TblVentum Venta)
        {
            try
            {
                if (Venta == null || Venta.IdVenta <= 0)
                {
                    return BadRequest("Se requiere proporcionar una Venta válida para actualizar.");
                }

                var resultado = _Venta.ActualizarVenta(Venta);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La Venta se actualizó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = "La Venta no pudo ser encontrada para actualizar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al actualizar la Venta: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al actualizar la Venta." });
            }
        }
    }
}
