using Microsoft.AspNetCore.Mvc;
using POS.Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace POS.Web.Controllers
{
    public class SucursalController : Controller
    {
        private readonly HttpClient _httpClient;
        public SucursalController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5014/");
        }
        public async Task<ActionResult> Index()
        {
            var response = await _httpClient.GetAsync("http://localhost:5014/Sucursals");
            if (response.IsSuccessStatusCode)
            { // 200 code

                var content = await response.Content.ReadAsStringAsync();
                var Sucursal = JsonConvert.DeserializeObject<IEnumerable<SucursalViewModel>>(content);
                return View("Index", Sucursal);

            }
            return View(new List<SucursalViewModel>());
        }
        public async Task<IActionResult> Details(int id)
        {

            var response = await _httpClient.GetAsync($"http://localhost:5014/Sucursal/getSucursalById?IdSucursal={id}");


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Sucursal = JsonConvert.DeserializeObject<SucursalViewModel>(content);

                // Devuelve la vista de edición con los detalles del Sucursal.
                return View(Sucursal);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del Sucursal.
                return RedirectToAction("Details"); // Redirige a la página de lista de Sucursals u otra acción apropiada.
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SucursalViewModel Sucursal)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(Sucursal);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //var response = await _httpClient.PostAsync("http://localhost:5014/Sucursal/guardar", content);
                var response = await _httpClient.PostAsync("/Sucursal/guardar", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de creación exitosa.
                    return RedirectToAction("Index");
                }
                else
                {
                    // Manejar el caso de error en la solicitud POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al crear el Sucursal.");
                }
            }
            return View(Sucursal);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var response = await _httpClient.GetAsync($"/Sucursal/getSucursalById?IdSucursal={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Sucursal = JsonConvert.DeserializeObject<SucursalViewModel>(content);

                // Devuelve la vista de edición con los detalles del Sucursal.
                return View(Sucursal);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del Sucursal.
                return RedirectToAction("Details"); // Redirige a la página de lista de Sucursals u otra acción apropiada.
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SucursalViewModel Sucursal)
        {
            if (ModelState.IsValid)
            {


                var json = JsonConvert.SerializeObject(Sucursal);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/Sucursals/actualizar?idSucursal={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de actualización exitosa, por ejemplo, redirigiendo a la página de detalles del Sucursal.
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    // Manejar el caso de error en la solicitud PUT o POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al actualizar el Sucursal.");
                }
            }

            // Si hay errores de validación, vuelve a mostrar el formulario de edición con los errores.
            return View(Sucursal);
        }

        public async Task<IActionResult> Delete(int id)
        {
            //var response = await _httpClient.DeleteAsync($"http://localhost:5014/Sucursals/eliminar?IdSucursal={id}");
            var response = await _httpClient.DeleteAsync($"/Sucursals/eliminar?IdSucursal={id}");

            if (response.IsSuccessStatusCode)
            {
                // Maneja el caso de eliminación exitosa, por ejemplo, redirigiendo a la página de lista de Sucursals.
                return RedirectToAction("Index");
            }
            else
            {
                // Maneja el caso de error en la solicitud DELETE, por ejemplo, mostrando un mensaje de error.
                TempData["Error"] = "Error al eliminar el Sucursal.";
                return RedirectToAction("Index");
            }
        }

    }
}
