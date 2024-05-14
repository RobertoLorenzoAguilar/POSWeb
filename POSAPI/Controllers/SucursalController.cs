using Datos.Models;
using Microsoft.AspNetCore.Mvc;
using Negocios.Interfaces;

namespace WebContratos.Controllers
{
    public class SucursalController : Controller
    {
        ISucursal _Sucursal;
        public SucursalController(ISucursal _Sucursal)
        {
            this._Sucursal = _Sucursal;
        }

        [HttpGet]
        [Route("Sucursals")]
        public IActionResult GetSucursals()
        {
            var resultado = _Sucursal.GetSucursals();

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de Sucursals
                return Ok(resultado.Sucursals); // Devolver un código 200 OK con la lista de Sucursals
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpGet]
        [Route("Sucursal/getSucursalById")]
        public IActionResult GetSucursalById(int IdSucursal)
        {
            var resultado = _Sucursal.GetSucursal(IdSucursal);

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de Sucursals
                return Ok(resultado.Sucursal); // Devolver un código 200 OK con la lista de Sucursals
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpPost]
        [Route("Sucursal/guardar")]
        public IActionResult AgregarSucursal([FromBody] TblSucursal Sucursal)
        {
            try
            {
                // Validar la entrada recibida
                if (Sucursal == null)
                {
                    return BadRequest("La solicitud no contiene datos válidos para la Sucursal.");
                }

                // Intentar guardar la Sucursal
                var resultado = _Sucursal.GuardarSucursal(Sucursal);

                if (resultado.Success)
                {
                    // Devolver un código 201 Created con la Sucursal creada
                    return CreatedAtAction(nameof(GetSucursalById), new { id = Sucursal.IdSucursal }, Sucursal);
                }
                else
                {
                    // Devolver un código 400 Bad Request con el mensaje de error
                    return BadRequest($"Error al guardar la Sucursal: {resultado.ErrorMessage}");
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
        [Route("Sucursals/eliminar")]
        public IActionResult EliminarSucursal(int IdSucursal)
        {
            try
            {
                if (IdSucursal == null || IdSucursal <= 0)
                {
                    return BadRequest("Se requiere proporcionar una Sucursal válida para eliminar.");
                }

                var resultado = _Sucursal.EliminarSucursal(IdSucursal);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La Sucursal se eliminó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = "La Sucursal no pudo ser encontrada para eliminar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al eliminar la Sucursal: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al eliminar la Sucursal." });
            }
        }

        [HttpPut]
        [Route("Sucursals/actualizar")]
        public IActionResult ActualizarSucursal([FromBody] TblSucursal Sucursal)
        {
            try
            {
                if (Sucursal == null || Sucursal.IdSucursal <= 0)
                {
                    return BadRequest("Se requiere proporcionar una Sucursal válida para actualizar.");
                }

                var resultado = _Sucursal.ActualizarSucursal(Sucursal);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La Sucursal se actualizó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = "La Sucursal no pudo ser encontrada para actualizar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al actualizar la Sucursal: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al actualizar la Sucursal." });
            }
        }
    }
}
