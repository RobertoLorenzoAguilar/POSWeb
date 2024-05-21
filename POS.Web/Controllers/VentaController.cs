using Microsoft.AspNetCore.Mvc;
using POS.Web.Models;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;

namespace POS.Web.Controllers
{
    public class VentaController : Controller
    {
        private readonly HttpClient _httpClient;
        public VentaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5014/");
        }
        public async Task<ActionResult> Index()
        {
            var response = await _httpClient.GetAsync("http://localhost:5014/Ventas");
            if (response.IsSuccessStatusCode)
            { // 200 code

                var content = await response.Content.ReadAsStringAsync();
                var Ventas = JsonConvert.DeserializeObject<IEnumerable<VentaViewModel>>(content);
                return View("Index", Ventas);

            }
            return View(new List<VentaViewModel>());
        }
        public async Task<IActionResult> Details(int id)
        {

            var response = await _httpClient.GetAsync($"http://localhost:5014/Venta/getVentaById?IdVenta={id}");


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Venta = JsonConvert.DeserializeObject<VentaViewModel>(content);

                // Devuelve la vista de edición con los detalles del Venta.
                return View(Venta);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del Venta.
                return RedirectToAction("Details"); // Redirige a la página de lista de Ventas u otra acción apropiada.
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VentaViewModel Venta)
        {
            if (ModelState.IsValid)
            {
              

                // Deserialización manual verificar problemas que puedan afectar este comportamiento
                var listaProductosEntry = ModelState["LstProducto"];
                if (listaProductosEntry != null && listaProductosEntry.RawValue != null)
                {
                    var listaProductosJson = listaProductosEntry.RawValue.ToString();
                    // Aquí puedes continuar con la deserialización de listaProductosJson
                    Venta.LstProducto = JsonConvert.DeserializeObject<List<ProductoCantidad>>(listaProductosJson);
                }


                var json = JsonConvert.SerializeObject(Venta);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //var response = await _httpClient.PostAsync("http://localhost:5014/Venta/guardar", content);
                var response = await _httpClient.PostAsync("/Venta/guardar", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de creación exitosa.
                    return RedirectToAction("Index");
                }
                else
                {
                    // Manejar el caso de error en la solicitud POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al crear el Venta.");
                }
            }
            return View(Venta);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var response = await _httpClient.GetAsync($"/Venta/getVentaById?IdVenta={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Venta = JsonConvert.DeserializeObject<VentaViewModel>(content);

                // Devuelve la vista de edición con los detalles del Venta.
                return View(Venta);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del Venta.
                return RedirectToAction("Details"); // Redirige a la página de lista de Ventas u otra acción apropiada.
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, VentaViewModel Venta)
        {
            if (ModelState.IsValid)
            {


                var json = JsonConvert.SerializeObject(Venta);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/Ventas/actualizar?idVenta={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de actualización exitosa, por ejemplo, redirigiendo a la página de detalles del Venta.
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    // Manejar el caso de error en la solicitud PUT o POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al actualizar el Venta.");
                }
            }

            // Si hay errores de validación, vuelve a mostrar el formulario de edición con los errores.
            return View(Venta);
        }

        public async Task<IActionResult> Delete(int id)
        {
            //var response = await _httpClient.DeleteAsync($"http://localhost:5014/Ventas/eliminar?IdVenta={id}");
            var response = await _httpClient.DeleteAsync($"/Ventas/eliminar?IdVenta={id}");

            if (response.IsSuccessStatusCode)
            {
                // Maneja el caso de eliminación exitosa, por ejemplo, redirigiendo a la página de lista de Ventas.
                return RedirectToAction("Index");
            }
            else
            {
                // Maneja el caso de error en la solicitud DELETE, por ejemplo, mostrando un mensaje de error.
                TempData["Error"] = "Error al eliminar el Venta.";
                return RedirectToAction("Index");
            }
        }

        //public async Task<IActionResult> AgregarDetalle(List<int>  lstInt )
        //{


        //    var a = Models.VentaViewModel;

        //    // Aquí puedes trabajar con los datos recibidos y actualizar el modelo existente
        //    var model = new VentaViewModel
        //    {
        //        LstProducto = lstInt
        //    };


        //    if (ModelState.IsValid)
        //    {

        //    }

        //        //var response = await _httpClient.DeleteAsync($"http://localhost:5014/Ventas/eliminar?IdVenta={id}");
        //        var response = await _httpClient.DeleteAsync($"/Ventas/eliminar?IdVent");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        // Maneja el caso de eliminación exitosa, por ejemplo, redirigiendo a la página de lista de Ventas.
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        // Maneja el caso de error en la solicitud DELETE, por ejemplo, mostrando un mensaje de error.
        //        TempData["Error"] = "Error al eliminar el Venta.";
        //        return RedirectToAction("Index");
        //    }
        //}

    }
}
