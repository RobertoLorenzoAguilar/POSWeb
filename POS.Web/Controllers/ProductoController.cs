using Microsoft.AspNetCore.Mvc;
using POS.Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace POS.Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly HttpClient _httpClient;
        public ProductoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5014/");
        }
        public async Task<ActionResult> Index()
        {
            var response = await _httpClient.GetAsync("http://localhost:5014/Productos");
            if (response.IsSuccessStatusCode)
            { // 200 code

                var content = await response.Content.ReadAsStringAsync();
                var productos = JsonConvert.DeserializeObject<IEnumerable<ProductoViewModel>>(content);
                return View("Index", productos);

            }
            return View(new List<ProductoViewModel>());
        }
        public async Task<IActionResult> Details(int id)
        {

            var response = await _httpClient.GetAsync($"http://localhost:5014/Producto/getProductoById?IdProducto={id}");


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var producto = JsonConvert.DeserializeObject<ProductoViewModel>(content);

                // Devuelve la vista de edición con los detalles del producto.
                return View(producto);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del producto.
                return RedirectToAction("Details"); // Redirige a la página de lista de productos u otra acción apropiada.
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoViewModel producto)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //var response = await _httpClient.PostAsync("http://localhost:5014/Producto/guardar", content);
                var response = await _httpClient.PostAsync("/Producto/guardar", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de creación exitosa.
                    return RedirectToAction("Index");
                }
                else
                {
                    // Manejar el caso de error en la solicitud POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al crear el producto.");
                }
            }
            return View(producto);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var response = await _httpClient.GetAsync($"/Producto/getProductoById?IdProducto={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var producto = JsonConvert.DeserializeObject<ProductoViewModel>(content);

                // Devuelve la vista de edición con los detalles del producto.
                return View(producto);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del producto.
                return RedirectToAction("Details"); // Redirige a la página de lista de productos u otra acción apropiada.
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductoViewModel producto)
        {
            if (ModelState.IsValid)
            {


                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/Productos/actualizar?idProducto={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de actualización exitosa, por ejemplo, redirigiendo a la página de detalles del producto.
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    // Manejar el caso de error en la solicitud PUT o POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al actualizar el producto.");
                }
            }

            // Si hay errores de validación, vuelve a mostrar el formulario de edición con los errores.
            return View(producto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            //var response = await _httpClient.DeleteAsync($"http://localhost:5014/Productos/eliminar?IdProducto={id}");
            var response = await _httpClient.DeleteAsync($"/Productos/eliminar?IdProducto={id}");

            if (response.IsSuccessStatusCode)
            {
                // Maneja el caso de eliminación exitosa, por ejemplo, redirigiendo a la página de lista de productos.
                return RedirectToAction("Index");
            }
            else
            {
                // Maneja el caso de error en la solicitud DELETE, por ejemplo, mostrando un mensaje de error.
                TempData["Error"] = "Error al eliminar el producto.";
                return RedirectToAction("Index");
            }
        }

    }
}
