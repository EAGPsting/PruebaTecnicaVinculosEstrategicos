using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using PortalVisitantes.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

namespace PortalVisitantes.Servicios
{
    public class Servicio_Api : Iservicio_Api
    {
        private static string _base;

        public Servicio_Api()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _base = builder.GetSection("ApiSetting:baseUrl").Value;
        }
        public async Task<List<Visitante>> Listar()
        {
            List<Visitante> lista = new List<Visitante>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_base);

                // Realizar la solicitud GET al endpoint
                var response = await cliente.GetAsync("api/visitantes");

                if (response.IsSuccessStatusCode)
                {
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    lista = JsonConvert.DeserializeObject<List<Visitante>>(json_respuesta);
                }
                else
                {
                    throw new Exception($"Error en la solicitud: {response.StatusCode}");
                }
            }

            return lista;
        }

        public async Task<Visitante> BuscarPorId(string dui)
        {
            Visitante objeto = null; // Inicializar como null en lugar de crear un nuevo objeto vacío

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_base);

                // Realizar la solicitud GET al endpoint
                var response = await cliente.GetAsync($"api/visitantes/{dui}");

                if (response.IsSuccessStatusCode)
                {
                    // Obtener la respuesta como un string JSON
                    var json_respuesta = await response.Content.ReadAsStringAsync();

                    // Deserializar directamente a un objeto Visitante
                    objeto = JsonConvert.DeserializeObject<Visitante>(json_respuesta);
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


        public async Task<bool> Guardar(Visitante c)
        {

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_base);

                var content = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");

                // Realizar la solicitud POST al endpoint
                var response = await cliente.PostAsync("api/visitantes/", content);

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

        public async Task<bool> Actualizar(Visitante c)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_base);

                var content = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");

                // Realizar la solicitud POST al endpoint
                var response = await cliente.PutAsync("api/visitantes/", content);

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

        public async Task<bool> Eliminar(string dui)
        {
            bool respuesta = false;
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_base);

                // Realizar la solicitud POST al endpoint
                var response = await cliente.DeleteAsync($"api/visitantes/{dui}");

                if (response.IsSuccessStatusCode)
                {
                    respuesta=true;
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