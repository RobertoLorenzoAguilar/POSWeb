using Microsoft.AspNetCore.Mvc;
using POS.Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace POS.Web.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly HttpClient _httpClient;
        public EmpresaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5014/");
        }
        public async Task<ActionResult> Index()
        {
            var response = await _httpClient.GetAsync("http://localhost:5014/Empresas");
            if (response.IsSuccessStatusCode)
            { // 200 code

                var content = await response.Content.ReadAsStringAsync();
                var Empresas = JsonConvert.DeserializeObject<IEnumerable<EmpresaViewModel>>(content);
                return View("Index", Empresas);

            }
            return View(new List<EmpresaViewModel>());
        }
        public async Task<IActionResult> Details(int id)
        {

            var response = await _httpClient.GetAsync($"http://localhost:5014/Empresa/getEmpresaById?IdEmpresa={id}");


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Empresa = JsonConvert.DeserializeObject<EmpresaViewModel>(content);

                // Devuelve la vista de edición con los detalles del Empresa.
                return View(Empresa);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del Empresa.
                return RedirectToAction("Details"); // Redirige a la página de lista de Empresas u otra acción apropiada.
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmpresaViewModel Empresa)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(Empresa);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //var response = await _httpClient.PostAsync("http://localhost:5014/Empresa/guardar", content);
                var response = await _httpClient.PostAsync("/Empresa/guardar", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de creación exitosa.
                    return RedirectToAction("Index");
                }
                else
                {
                    // Leer el contenido de la respuesta de error
                    var errorContent = await response.Content.ReadAsStringAsync();
                    // Manejar el caso de error en la solicitud POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, errorContent);
                }
            }
            return View(Empresa);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var response = await _httpClient.GetAsync($"/Empresa/getEmpresaById?IdEmpresa={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Empresa = JsonConvert.DeserializeObject<EmpresaViewModel>(content);

                // Devuelve la vista de edición con los detalles del Empresa.
                return View(Empresa);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del Empresa.
                return RedirectToAction("Details"); // Redirige a la página de lista de Empresas u otra acción apropiada.
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EmpresaViewModel Empresa)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(Empresa);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/Empresas/actualizar?idEmpresa={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de actualización exitosa, por ejemplo, redirigiendo a la página de detalles del Empresa.
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    // Manejar el caso de error en la solicitud PUT o POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al actualizar el Empresa.");
                }
            }

            // Si hay errores de validación, vuelve a mostrar el formulario de edición con los errores.
            return View(Empresa);
        }

        public async Task<IActionResult> Delete(int id)
        {
            //var response = await _httpClient.DeleteAsync($"http://localhost:5014/Empresas/eliminar?IdEmpresa={id}");
            var response = await _httpClient.DeleteAsync($"/Empresas/eliminar?IdEmpresa={id}");

            if (response.IsSuccessStatusCode)
            {
                // Maneja el caso de eliminación exitosa, por ejemplo, redirigiendo a la página de lista de Empresas.
                return RedirectToAction("Index");
            }
            else
            {
                // Maneja el caso de error en la solicitud DELETE, por ejemplo, mostrando un mensaje de error.
                TempData["Error"] = "Error al eliminar el Empresa.";
                return RedirectToAction("Index");
            }
        }

    }
}
