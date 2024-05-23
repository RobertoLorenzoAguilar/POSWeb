using Datos.Models;
using Microsoft.AspNetCore.Mvc;
using Negocios.Interfaces;

namespace WebContratos.Controllers
{
    public class EmpleadoController : Controller
    {
        IEmpleado _Empleado;
        public EmpleadoController(IEmpleado _Empleado)
        {
            this._Empleado = _Empleado;
        }

        [HttpGet]
        [Route("Empleados")]
        public IActionResult GetEmpleados()
        {
            var resultado = _Empleado.GetEmpleados();

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de Empleados
                return Ok(resultado.Empleados); // Devolver un código 200 OK con la lista de Empleados
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpGet]
        [Route("Empleado/GetEmpleadoTipo")]
        public IActionResult GetEmpleadoTipo()
        {
            var resultado = _Empleado.GetEmpleadosPorTipo("Asesor de Venta");

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de Empleados
                return Ok(resultado.Empleados); // Devolver un código 200 OK con la lista de Empleados
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpGet]
        [Route("Empleado/getEmpleadoById")]
        public IActionResult GetEmpleadoById(int IdEmpleado)
        {
            var resultado = _Empleado.GetEmpleado(IdEmpleado);

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de Empleados
                return Ok(resultado.Empleado); // Devolver un código 200 OK con la lista de Empleados
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpPost]
        [Route("Empleado/guardar")]
        public IActionResult AgregarEmpleado([FromBody] TblEmpleado Empleado)
        {
            try
            {
                // Validar la entrada recibida
                if (Empleado == null)
                {
                    return BadRequest("La solicitud no contiene datos válidos para la Empleado.");
                }

                // Intentar guardar la Empleado
                var resultado = _Empleado.GuardarEmpleado(Empleado);

                if (resultado.Success)
                {
                    // Devolver un código 201 Created con la Empleado creada
                    return CreatedAtAction(nameof(GetEmpleadoById), new { id = Empleado.IdEmpleado }, Empleado);
                }
                else
                {
                    // Devolver un código 400 Bad Request con el mensaje de error
                    return BadRequest($"Error al guardar la Empleado: {resultado.ErrorMessage}");
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
        [Route("Empleados/eliminar")]
        public IActionResult EliminarEmpleado(int IdEmpleado)
        {
            try
            {
                if (IdEmpleado == null || IdEmpleado <= 0)
                {
                    return BadRequest("Se requiere proporcionar una Empleado válida para eliminar.");
                }

                var resultado = _Empleado.EliminarEmpleado(IdEmpleado);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La Empleado se eliminó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = "La Empleado no pudo ser encontrada para eliminar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al eliminar la Empleado: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al eliminar la Empleado." });
            }
        }

        [HttpPut]
        [Route("Empleados/actualizar")]
        public IActionResult ActualizarEmpleado([FromBody] TblEmpleado Empleado)
        {
            try
            {
                if (Empleado == null || Empleado.IdEmpleado <= 0)
                {
                    return BadRequest("Se requiere proporcionar una Empleado válida para actualizar.");
                }

                var resultado = _Empleado.ActualizarEmpleado(Empleado);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La Empleado se actualizó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = $"Error al actualizar el Empleado: {resultado.ErrorMessage}" });
                    //return NotFound(new { Mensaje = "La Empleado no pudo ser encontrada para actualizar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al actualizar la Empleado: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al actualizar la Empleado." });
            }
        }
    }
}
