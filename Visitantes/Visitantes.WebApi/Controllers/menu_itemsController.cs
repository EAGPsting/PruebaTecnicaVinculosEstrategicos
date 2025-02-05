using Oracle.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Visitantes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class menu_itemsController : ControllerBase
    {
        private ModelContext _context;

        public menu_itemsController(ModelContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<MenuItem>>> Listar()
        {
            var menuItems = await _context.MenuItems.ToListAsync();
            return Ok(menuItems); // Devuelve la lista de MenuItems sin relaciones circulares
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> BuscarPorId(int id)
        {
            var retorno = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == id);

            if (retorno != null)
            {
                return retorno;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<MenuItem>> Guardar(MenuItem c)
        {
           
            try
            {                
                await _context.MenuItems.AddAsync(c);
                await _context.SaveChangesAsync();


                return c;
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Se encontro un error");
            }
        }

        [HttpPut]
        public async Task<ActionResult<MenuItem>> Actualizar(MenuItem c)
        {
            if (c == null || c.Id == 0)
                return BadRequest("Debe ingresar un ID");

            MenuItem cat = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == c.Id);

            if (cat == null)
                return NotFound();

            try
            {
                cat.Nombre = c.Nombre;
                cat.Url = c.Url;
                cat.PadreId = c.PadreId;
                _context.MenuItems.Update(cat);
                await _context.SaveChangesAsync();

                return cat;
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Se encontro un error al actualizar");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Eliminar(int id)
        {
            MenuItem cat = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == id);

            if (cat == null)
                return NotFound();

            try
            {
                _context.MenuItems.Remove(cat);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Se encontro un error al eliminar");
            }

        }
    }
}
