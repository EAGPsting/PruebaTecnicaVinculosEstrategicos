using PortalVisitantes.Models;

namespace PortalVisitantes.Servicios
{   
    public interface Iservicio_Api
    {
        Task<List<Visitante>> Listar();
        Task<Visitante> BuscarPorId(string dui);
        Task<bool> Guardar(Visitante c);
        Task<bool> Actualizar(Visitante c);
        Task<bool> Eliminar(string dui);      
    }
}
