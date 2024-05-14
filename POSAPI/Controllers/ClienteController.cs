using Datos.Models;
using Microsoft.AspNetCore.Mvc;
using Negocios.Interfaces;

namespace WebContratos.Controllers
{
    public class ClienteController : Controller
    {
        ICliente _Cliente;
        public ClienteController(ICliente _Cliente)
        {
            this._Cliente = _Cliente;
        }

        [HttpGet]
        [Route("Clientes")]
        public IActionResult GetClientes()
        {
            var resultado = _Cliente.GetClientes();

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de Clientes
                return Ok(resultado.Clientes); // Devolver un código 200 OK con la lista de Clientes
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpGet]
        [Route("Cliente/getClienteById")]
        public IActionResult GetClienteById(int IdCliente)
        {
            var resultado = _Cliente.GetCliente(IdCliente);

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de Clientes
                return Ok(resultado.Cliente); // Devolver un código 200 OK con la lista de Clientes
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpPost]
        [Route("Cliente/guardar")]
        public IActionResult AgregarCliente([FromBody] TblCliente Cliente)
        {
            try
            {
                // Validar la entrada recibida
                if (Cliente == null)
                {
                    return BadRequest("La solicitud no contiene datos válidos para la Cliente.");
                }

                // Intentar guardar la Cliente
                var resultado = _Cliente.GuardarCliente(Cliente);

                if (resultado.Success)
                {
                    // Devolver un código 201 Created con la Cliente creada
                    return CreatedAtAction(nameof(GetClienteById), new { id = Cliente.IdCliente }, Cliente);
                }
                else
                {
                    // Devolver un código 400 Bad Request con el mensaje de error
                    return BadRequest($"Error al guardar la Cliente: {resultado.ErrorMessage}");
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
        [Route("Clientes/eliminar")]
        public IActionResult EliminarCliente(int IdCliente)
        {
            try
            {
                if (IdCliente == null || IdCliente <= 0)
                {
                    return BadRequest("Se requiere proporcionar una Cliente válida para eliminar.");
                }

                var resultado = _Cliente.EliminarCliente(IdCliente);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La Cliente se eliminó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = "La Cliente no pudo ser encontrada para eliminar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al eliminar la Cliente: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al eliminar la Cliente." });
            }
        }

        [HttpPut]
        [Route("Clientes/actualizar")]
        public IActionResult ActualizarCliente([FromBody] TblCliente Cliente)
        {
            try
            {
                if (Cliente == null || Cliente.IdCliente <= 0)
                {
                    return BadRequest("Se requiere proporcionar una Cliente válida para actualizar.");
                }

                var resultado = _Cliente.ActualizarCliente(Cliente);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La Cliente se actualizó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = "La Cliente no pudo ser encontrada para actualizar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al actualizar la Cliente: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al actualizar la Cliente." });
            }
        }
    }
}
