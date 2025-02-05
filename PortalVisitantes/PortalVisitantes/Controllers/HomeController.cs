using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PortalVisitantes.Models;
using PortalVisitantes.Servicios;
namespace PortalVisitantes.Controllers
{
    public class HomeController : Controller
    {
        private readonly Iservicio_Api _serviciosApi;

        public HomeController(Iservicio_Api servicioApi)
        {
            _serviciosApi = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            List<Visitante> Lista = await _serviciosApi.Listar();
            return View(Lista);
        }

        public async Task<IActionResult> Visitante(string dui)
        {
            Visitante modelo_visitante = new Visitante();
            ViewBag.Accion = "Nuevo Visitante";

            if (dui != "" && dui != null)
            {

                ViewBag.Accion = "Editar Producto";
                modelo_visitante = await _serviciosApi.BuscarPorId(dui);
            }

            return View(modelo_visitante);
        }


        [HttpPost]
        public async Task<IActionResult> GuardarCambios(Visitante ob_visitante)
        {

            bool respuesta;

            if (ob_visitante.Generacion == "" || ob_visitante.Generacion == null)
            {
                respuesta = await _serviciosApi.Guardar(ob_visitante);
            }
            else
            {
                respuesta = await _serviciosApi.Actualizar(ob_visitante);
            }


            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(string dui)
        {

            var respuesta = await _serviciosApi.Eliminar(dui);

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
