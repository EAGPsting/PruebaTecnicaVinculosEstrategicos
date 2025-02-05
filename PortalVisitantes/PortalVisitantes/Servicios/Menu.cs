using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using PortalVisitantes.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

namespace PortalVisitantes.Servicios
{
    public class Menu: IMenucs
    {
        private static string _base;

        public Menu()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _base = builder.GetSection("ApiSetting:baseUrl").Value;
        }
        public async Task<List<MenuItem>> ListarMenu()
        {
            List<MenuItem> lista = new List<MenuItem>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_base);

                // Realizar la solicitud GET al endpoint
                var response = await cliente.GetAsync("api/menu_items");

                if (response.IsSuccessStatusCode)
                {
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    lista = JsonConvert.DeserializeObject<List<MenuItem>>(json_respuesta);
                }
                else
                {
                    throw new Exception($"Error en la solicitud: {response.StatusCode}");
                }
            }

            return lista;
        }

        public async Task<MenuItem> BuscarMenuPorId(int id)
        {
            MenuItem objeto = null; // Inicializar como null en lugar de crear un nuevo objeto vacío

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_base);

                // Realizar la solicitud GET al endpoint
                var response = await cliente.GetAsync($"api/menu_items/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // Obtener la respuesta como un string JSON
                    var json_respuesta = await response.Content.ReadAsStringAsync();

                    // Deserializar directamente a un objeto Visitante
                    objeto = JsonConvert.DeserializeObject<MenuItem>(json_respuesta);
                }
                else
                {
                    // Si no fue exitosa, obtener el contenido del error para diagnóstico
                    var errorDetalle = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error en la solicitud: {response.StatusCode}. Detalles: {errorDetalle}");
                }
            }

            return objeto;
        }


        public async Task<bool> GuardarMenu(MenuItem c)
        {

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_base);

                var content = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");

                // Realizar la solicitud POST al endpoint
                var response = await cliente.PostAsync("api/menu_items/", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Error en la solicitud: {response.StatusCode}");
                }
            }
        }

        public async Task<bool> ActualizarMenu(MenuItem c)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_base);

                var content = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");

                // Realizar la solicitud POST al endpoint
                var response = await cliente.PutAsync("api/menu_items/", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Error en la solicitud: {response.StatusCode}");
                }
            }
        }

        public async Task<bool> EliminarMenu(int id)
        {
            bool respuesta = false;
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_base);

                // Realizar la solicitud POST al endpoint
                var response = await cliente.DeleteAsync($"api/menu_items/{id}");

                if (response.IsSuccessStatusCode)
                {
                    respuesta = true;
                }
                else
                {
                    throw new Exception($"Error en la solicitud: {response.StatusCode}");
                }
                return respuesta;
            }
        }
    }
}
