using Oracle.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

namespace Visitantes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class visitantesController : ControllerBase
    {
        private ModelContext _context;

        public visitantesController(ModelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Visitante>>> Listar()
        {
            return await _context.Visitantes.ToListAsync();
        }

        [HttpGet("{dui}")]
        public async Task<ActionResult<Visitante>> BuscarPorId(string dui)
        {
            var retorno = await _context.Visitantes.FirstOrDefaultAsync(x => x.Dui == dui);

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
        public async Task<ActionResult<Visitante>> Guardar(Visitante c)
        {
            DateTime anio;
            try
            {
                anio = (DateTime)c.FechaNacimiento;
                c.Generacion = DeterminarGeneracion(anio.Year);
                await _context.Visitantes.AddAsync(c);
                await _context.SaveChangesAsync();
                

                return c;
            }
            catch(DbUpdateException)
            {
                return StatusCode(500, "Se encontro un error");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Visitante>> Actualizar(Visitante c)
        {
            DateTime anio;
            if (c == null || c.Dui == "")
                return BadRequest("Debe ingresar un DUI");

            Visitante cat = await _context.Visitantes.FirstOrDefaultAsync(x => x.Dui == c.Dui);

            if (cat == null)
                return NotFound();

            try
            {
                anio = (DateTime)c.FechaNacimiento;
                cat.Nombre = c.Nombre;
                cat.Email = c.Email;
                cat.FechaNacimiento = c.FechaNacimiento;
                cat.Telefono = c.Telefono;
                cat.Generacion = DeterminarGeneracion(anio.Year);
                _context.Visitantes.Update(cat);
                await _context.SaveChangesAsync();

                return cat;
            }
            catch (DbUpdateException) 
            {
                return StatusCode(500, "Se encontro un error al actualizar");
            }
        }

        [HttpDelete("{dui}")]
        public async Task<ActionResult<bool>> Eliminar(string dui)
        {
            Visitante cat = await _context.Visitantes.FirstOrDefaultAsync(x => x.Dui == dui);

            if (cat == null)
                return NotFound();

            try
            {
                _context.Visitantes.Remove(cat);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Se encontro un error al eliminar");
            }

        }


        [HttpGet("exportar")]
        public async Task<IActionResult> ExportarVisitantes([FromQuery] string formato)
        {
            var visitantes = await _context.Visitantes.ToListAsync();

            if (visitantes == null || visitantes.Count == 0)
                return NotFound("No hay visitantes para exportar.");

            if (formato.ToLower() == "json")
            {
                var json = JsonSerializer.Serialize(visitantes, new JsonSerializerOptions { WriteIndented = true });
                var bytes = Encoding.UTF8.GetBytes(json);
                return File(bytes, "application/json", "visitantes.json");
            }
            else if (formato.ToLower() == "xml")
            {
                var serializer = new XmlSerializer(typeof(List<Visitante>));
                using var stream = new MemoryStream();
                using var writer = new StreamWriter(stream, Encoding.UTF8);
                serializer.Serialize(writer, visitantes);
                return File(stream.ToArray(), "application/xml", "visitantes.xml");
            }

            return BadRequest("Formato no válido. Use 'json' o 'xml'.");
        }
        static string DeterminarGeneracion(int anio)
        {
            if (anio >= 1949 && anio <= 1968)
                return "Baby Boomers";
            else if (anio >= 1969 && anio <= 1980)
                return "Generación X";
            else if (anio >= 1981 && anio <= 1993)
                return "Millennials";
            else if (anio >= 1994 && anio <= 2010)
                return "Generación Z";
            else if (anio > 2010)
                return "Generación Alpha";
            else
                return "Año fuera de rango";
        }

    }
}
