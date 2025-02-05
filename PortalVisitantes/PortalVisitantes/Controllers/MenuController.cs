using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PortalVisitantes.Models;
using PortalVisitantes.Servicios;

namespace PortalVisitantes.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenucs _serviciosApi;

        public MenuController(IMenucs servicioApi)
        {
            _serviciosApi = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            List<MenuItem> Lista = await _serviciosApi.ListarMenu();
            return View(Lista);
        }
        public async Task<IActionResult> MostrarMenu()
        {
            // Obtener los elementos del menú desde el servicio o base de datos
            var menuItems = await _serviciosApi.ListarMenu();

            // Crear una estructura jerárquica del menú
            var menuTree = BuildMenuTree(menuItems);

            // Pasar el árbol de menú a la nueva vista
            return View(menuTree);
        }
        // Método auxiliar para construir la estructura jerárquica
        private List<MenuItem> BuildMenuTree(List<MenuItem> menuItems)
        {
            var menuDictionary = menuItems.ToDictionary(item => item.Id);
            var rootItems = new List<MenuItem>();

            foreach (var item in menuItems)
            {
                if (item.PadreId == null) // Es un elemento raíz
                {
                    rootItems.Add(item);
                }
                else if (menuDictionary.ContainsKey(item.PadreId.Value))
                {
                    var parent = menuDictionary[item.PadreId.Value];
                    if (parent.InversePadre == null)
                        parent.InversePadre = new List<MenuItem>();

                    parent.InversePadre.Add(item);
                }
            }

            return rootItems;
        }
        public async Task<IActionResult> Menu(int id)
        {
            MenuItem modelo_menu = new MenuItem();
            ViewBag.Accion = "Nuevo Menu";

            if (id != 0 && id != null)
            {

                ViewBag.Accion = "Editar menu";
                modelo_menu = await _serviciosApi.BuscarMenuPorId(id);
            }

            return View(modelo_menu);
        }


        [HttpPost]
        public async Task<IActionResult> GuardarCambios(MenuItem ob_menu)
        {

            bool respuesta;

            if (ob_menu.Id == 0 || ob_menu.Id == null)
            {
                respuesta = await _serviciosApi.GuardarMenu(ob_menu);
            }
            else
            {
                respuesta = await _serviciosApi.ActualizarMenu(ob_menu);
            }


            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {

            var respuesta = await _serviciosApi.EliminarMenu(id);

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
