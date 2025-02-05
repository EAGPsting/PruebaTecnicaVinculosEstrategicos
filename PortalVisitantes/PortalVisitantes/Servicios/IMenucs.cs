using PortalVisitantes.Models;

namespace PortalVisitantes.Servicios
{
    public interface IMenucs
    {
        Task<List<MenuItem>> ListarMenu();
        Task<MenuItem> BuscarMenuPorId(int id);
        Task<bool> GuardarMenu(MenuItem c);
        Task<bool> ActualizarMenu(MenuItem c);
        Task<bool> EliminarMenu(int id);
    }
}
