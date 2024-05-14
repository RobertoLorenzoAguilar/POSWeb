using Datos.Models;
using Microsoft.AspNetCore.Mvc;
using Negocios.Interfaces;

namespace WebContratos.Controllers
{
    public class EmpresaController : Controller
    {
        IEmpresa _Empresa;
        public EmpresaController(IEmpresa _Empresa)
        {
            this._Empresa = _Empresa;
        }

        [HttpGet]
        [Route("Empresas")]
        public IActionResult GetEmpresas()
        {
            var resultado = _Empresa.GetEmpresas();

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de empresas
                return Ok(resultado.Empresas); // Devolver un código 200 OK con la lista de empresas
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpGet]
        [Route("Empresa/getEmpresaById")]
        public IActionResult GetEmpresaById(int IdEmpresa)
        {
            var resultado = _Empresa.GetEmpresa(IdEmpresa);

            if (resultado.Success)
            {
                // Si la operación fue exitosa, retornar la lista de empresas
                return Ok(resultado.Empresa); // Devolver un código 200 OK con la lista de empresas
            }
            else
            {
                // Si hubo un error en la operación, devolver un código 500 Internal Server Error con el mensaje de error
                return StatusCode(500, new { ErrorMessage = resultado.ErrorMessage });
            }
        }

        [HttpPost]
        [Route("Empresa/guardar")]
        public IActionResult AgregarEmpresa([FromBody] TblEmpresa empresa)
        {
            try
            {
                // Validar la entrada recibida
                if (empresa == null)
                {
                    return BadRequest("La solicitud no contiene datos válidos para la empresa.");
                }

                // Intentar guardar la empresa
                var resultado = _Empresa.GuardarEmpresa(empresa);

                if (resultado.Success)
                {
                    // Devolver un código 201 Created con la empresa creada
                    return CreatedAtAction(nameof(GetEmpresaById), new { id = empresa.IdEmpresa }, empresa);
                }
                else
                {
                    // Devolver un código 400 Bad Request con el mensaje de error
                    return BadRequest($"Error al guardar la empresa: {resultado.ErrorMessage}");
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
        [Route("Empresas/eliminar")]
        public IActionResult EliminarEmpresa(int IdEmpresa)
        {
            try
            {
                if (IdEmpresa == null || IdEmpresa <= 0)
                {
                    return BadRequest("Se requiere proporcionar una empresa válida para eliminar.");
                }

                var resultado = _Empresa.EliminarEmpresa(IdEmpresa);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La empresa se eliminó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = "La empresa no pudo ser encontrada para eliminar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al eliminar la empresa: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al eliminar la empresa." });
            }
        }

        [HttpPut]
        [Route("Empresas/actualizar")]
        public IActionResult ActualizarEmpresa([FromBody] TblEmpresa empresa)
        {
            try
            {
                if (empresa == null || empresa.IdEmpresa <= 0)
                {
                    return BadRequest("Se requiere proporcionar una empresa válida para actualizar.");
                }

                var resultado = _Empresa.ActualizarEmpresa(empresa);

                if (resultado.Success)
                {
                    return Ok(new { Mensaje = "La empresa se actualizó correctamente." });
                }
                else
                {
                    return NotFound(new { Mensaje = "La empresa no pudo ser encontrada para actualizar." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error al actualizar la empresa: {ex.Message}");

                // Devolver un código 500 Internal Server Error con un mensaje genérico
                return StatusCode(500, new { Mensaje = "Se produjo un error interno al actualizar la empresa." });
            }
        }
    }
}
