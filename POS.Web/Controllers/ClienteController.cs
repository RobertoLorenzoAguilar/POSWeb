using Microsoft.AspNetCore.Mvc;
using POS.Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace POS.Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly HttpClient _httpClient;
        public ClienteController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5014/");
        }
        public async Task<ActionResult> Index()
        {
            var response = await _httpClient.GetAsync("http://localhost:5014/Clientes");
            if (response.IsSuccessStatusCode)
            { // 200 code

                var content = await response.Content.ReadAsStringAsync();
                var Clientes = JsonConvert.DeserializeObject<IEnumerable<ClienteViewModel>>(content);
                return View("Index", Clientes);

            }
            return View(new List<ClienteViewModel>());
        }
        public async Task<IActionResult> Details(int id)
        {

            var response = await _httpClient.GetAsync($"http://localhost:5014/Cliente/getClienteById?IdCliente={id}");


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Cliente = JsonConvert.DeserializeObject<ClienteViewModel>(content);

                // Devuelve la vista de edición con los detalles del Cliente.
                return View(Cliente);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del Cliente.
                return RedirectToAction("Details"); // Redirige a la página de lista de Clientes u otra acción apropiada.
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteViewModel Cliente)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(Cliente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //var response = await _httpClient.PostAsync("http://localhost:5014/Cliente/guardar", content);
                var response = await _httpClient.PostAsync("/Cliente/guardar", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de creación exitosa.
                    return RedirectToAction("Index");
                }
                else
                {
                    // Manejar el caso de error en la solicitud POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al crear el Cliente.");
                }
            }
            return View(Cliente);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var response = await _httpClient.GetAsync($"/Cliente/getClienteById?IdCliente={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Cliente = JsonConvert.DeserializeObject<ClienteViewModel>(content);

                // Devuelve la vista de edición con los detalles del Cliente.
                return View(Cliente);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del Cliente.
                return RedirectToAction("Details"); // Redirige a la página de lista de Clientes u otra acción apropiada.
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ClienteViewModel Cliente)
        {
            if (ModelState.IsValid)
            {


                var json = JsonConvert.SerializeObject(Cliente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/Clientes/actualizar?IdCliente={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de actualización exitosa, por ejemplo, redirigiendo a la página de detalles del Cliente.
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    // Manejar el caso de error en la solicitud PUT o POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al actualizar el Cliente.");
                }
            }

            // Si hay errores de validación, vuelve a mostrar el formulario de edición con los errores.
            return View(Cliente);
        }

        public async Task<IActionResult> Delete(int id)
        {
            //var response = await _httpClient.DeleteAsync($"http://localhost:5014/Clientes/eliminar?IdCliente={id}");
            var response = await _httpClient.DeleteAsync($"/Clientes/eliminar?IdCliente={id}");

            if (response.IsSuccessStatusCode)
            {
                // Maneja el caso de eliminación exitosa, por ejemplo, redirigiendo a la página de lista de Clientes.
                return RedirectToAction("Index");
            }
            else
            {
                // Maneja el caso de error en la solicitud DELETE, por ejemplo, mostrando un mensaje de error.
                TempData["Error"] = "Error al eliminar el Cliente.";
                return RedirectToAction("Index");
            }
        }

    }
}
