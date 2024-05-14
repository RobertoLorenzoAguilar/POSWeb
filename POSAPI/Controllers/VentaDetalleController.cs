using Datos.Models;
using Microsoft.AspNetCore.Mvc;
using Negocios.Interfaces;

namespace WebContratos.Controllers
{
    public class VentaDetalleController : Controller
    {
        IVentaDetalle _VentaDetalle;
        public VentaDetalleController(IVentaDetalle _VentaDetalle)
        {
            this._VentaDetalle = _VentaDetalle;
        }

        [HttpGet]
        [Route("VentaDetalles")]
        public IActionResult GetVentaDetalles()
        {
            var resultado = _VentaDetalle.GetVentaDetalles();

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de VentaDetalles
                return Ok(resultado.VentaDetalles); // Devolver un código 200 OK con la lista de VentaDetalles
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpGet]
        [Route("VentaDetalle/getVentaDetalleById")]
        public IActionResult GetVentaDetalleById(int IdVentaDetalle)
        {
            var resultado = _VentaDetalle.GetVentaDetalle(IdVentaDetalle);

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de VentaDetalles
                return Ok(resultado.VentaDetalle); // Devolver un código 200 OK con la lista de VentaDetalles
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpPost]
        [Route("VentaDetalle/guardar")]
        public IActionResult AgregarVentaDetalle([FromBody] TblVentaDetalle VentaDetalle)
        {
            try
            {
                // Validar la entrada recibida
                if (VentaDetalle == null)
                {
                    return BadRequest("La solicitud no contiene datos válidos para la VentaDetalle.");
                }

                // Intentar guardar la VentaDetalle
                var resultado = _VentaDetalle.GuardarVentaDetalle(VentaDetalle);

                if (resultado.Success)
                {
                    // Devolver un código 201 Created con la VentaDetalle creada
                    return CreatedAtAction(nameof(GetVentaDetalleById), new { id = VentaDetalle.IdVentaDetalle }, VentaDetalle);
                }
                else
                {
                    // Devolver un código 400 Bad Request con el mensaje de error
                    return BadRequest($"Error al guardar la VentaDetalle: {resultado.ErrorMessage}");
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
        [Route("VentaDetalles/eliminar")]
        public IActionResult EliminarVentaDetalle(int IdVentaDetalle)
        {
            try
            {
                if (IdVentaDetalle == null || IdVentaDetalle <= 0)
                {
                    return BadRequest("Se requiere proporcionar una VentaDetalle válida para eliminar.");
                }

                var resultado = _VentaDetalle.EliminarVentaDetalle(IdVentaDetalle);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La VentaDetalle se eliminó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = "La VentaDetalle no pudo ser encontrada para eliminar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al eliminar la VentaDetalle: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al eliminar la VentaDetalle." });
            }
        }

        [HttpPut]
        [Route("VentaDetalles/actualizar")]
        public IActionResult ActualizarVentaDetalle([FromBody] TblVentaDetalle VentaDetalle)
        {
            try
            {
                if (VentaDetalle == null || VentaDetalle.IdVentaDetalle <= 0)
                {
                    return BadRequest("Se requiere proporcionar una VentaDetalle válida para actualizar.");
                }

                var resultado = _VentaDetalle.ActualizarVentaDetalle(VentaDetalle);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La VentaDetalle se actualizó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = "La VentaDetalle no pudo ser encontrada para actualizar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al actualizar la VentaDetalle: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al actualizar la VentaDetalle." });
            }
        }
    }
}
