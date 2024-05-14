using Microsoft.AspNetCore.Mvc;
using POS.Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace POS.Web.Controllers
{
    public class ProductoSucursal : Controller
    {
        private readonly HttpClient _httpClient;
        public ProductoSucursal(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5014/");
        }
        public async Task<ActionResult> Index()
        {
            var response = await _httpClient.GetAsync("http://localhost:5014/Empleados");
            if (response.IsSuccessStatusCode)
            { // 200 code

                var content = await response.Content.ReadAsStringAsync();
                var Empleados = JsonConvert.DeserializeObject<IEnumerable<ProductoSucursalViewModel>>(content);
                return View("Index", Empleados);

            }
            return View(new List<ProductoSucursalViewModel>());
        }
        public async Task<IActionResult> Details(int id)
        {

            var response = await _httpClient.GetAsync($"http://localhost:5014/Empleado/getEmpleadoById?IdEmpleado={id}");


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Empleado = JsonConvert.DeserializeObject<ProductoSucursalViewModel>(content);

                // Devuelve la vista de edición con los detalles del Empleado.
                return View(Empleado);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del Empleado.
                return RedirectToAction("Details"); // Redirige a la página de lista de Empleados u otra acción apropiada.
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoSucursalViewModel Empleado)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(Empleado);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //var response = await _httpClient.PostAsync("http://localhost:5014/Empleado/guardar", content);
                var response = await _httpClient.PostAsync("/Empleado/guardar", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de creación exitosa.
                    return RedirectToAction("Index");
                }
                else
                {
                    // Manejar el caso de error en la solicitud POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al crear el Empleado.");
                }
            }
            return View(Empleado);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var response = await _httpClient.GetAsync($"/Empleado/getEmpleadoById?IdEmpleado={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Empleado = JsonConvert.DeserializeObject<ProductoSucursalViewModel>(content);

                // Devuelve la vista de edición con los detalles del Empleado.
                return View(Empleado);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del Empleado.
                return RedirectToAction("Details"); // Redirige a la página de lista de Empleados u otra acción apropiada.
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductoSucursalViewModel Empleado)
        {
            if (ModelState.IsValid)
            {


                var json = JsonConvert.SerializeObject(Empleado);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/Empleados/actualizar?idEmpleado={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de actualización exitosa, por ejemplo, redirigiendo a la página de detalles del Empleado.
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    // Manejar el caso de error en la solicitud PUT o POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al actualizar el Empleado.");
                }
            }

            // Si hay errores de validación, vuelve a mostrar el formulario de edición con los errores.
            return View(Empleado);
        }

        public async Task<IActionResult> Delete(int id)
        {
            //var response = await _httpClient.DeleteAsync($"http://localhost:5014/Empleados/eliminar?IdEmpleado={id}");
            var response = await _httpClient.DeleteAsync($"/Empleados/eliminar?IdEmpleado={id}");

            if (response.IsSuccessStatusCode)
            {
                // Maneja el caso de eliminación exitosa, por ejemplo, redirigiendo a la página de lista de Empleados.
                return RedirectToAction("Index");
            }
            else
            {
                // Maneja el caso de error en la solicitud DELETE, por ejemplo, mostrando un mensaje de error.
                TempData["Error"] = "Error al eliminar el Empleado.";
                return RedirectToAction("Index");
            }
        }

    }
}
