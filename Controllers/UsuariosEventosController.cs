using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using partyholic_api.Models;

namespace partyholic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosEventosController : ControllerBase
    {
        private readonly partyholicContext _context;

        public UsuariosEventosController(partyholicContext context)
        {
            _context = context;
        }

        // GET: api/UsuariosEventoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuariosEvento>>> GetUsuariosEventos()
        {
          if (_context.UsuariosEventos == null)
          {
              return NotFound();
          }
            return await _context.UsuariosEventos.ToListAsync();
        }

        [HttpGet("{codGrupo}/{username}")]
        public async Task<ActionResult<UsuariosEvento>> GetUsuarioEvento(int codGrupo, string username)
        {
            try
            {
                var usuarioEventos = await _context.UsuariosEventos
                       .Where(uv => uv.CodGrupo == codGrupo && uv.Username == username)
                       .ToListAsync();

                if (usuarioEventos == null || !usuarioEventos.Any())
                {
                    return Ok();
                }

                return Ok(usuarioEventos);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar eventos: " + ex.ToString());
            }

        }

        // GET: api/UsuariosEventoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuariosEvento>> GetUsuariosEvento(int id)
        {
          if (_context.UsuariosEventos == null)
          {
              return NotFound();
          }
            var usuariosEvento = await _context.UsuariosEventos.FindAsync(id);

            if (usuariosEvento == null)
            {
                return NotFound();
            }

            return usuariosEvento;
        }

        // PUT: api/UsuariosEventoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuariosEvento(int id, UsuariosEvento usuariosEvento)
        {
            if (id != usuariosEvento.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuariosEvento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosEventoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UsuariosEventoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("crear")]
        [HttpPost]
        public IActionResult CrearUsuariosEvento(UsuariosEvento usuariosEvento)
        {
            try
            {
                // Crear una nueva instancia de UsuariosEvento y copiar los datos desde el objeto enviado en la solicitud
                UsuariosEvento nuevoUsuariosEvento = new UsuariosEvento
                {
                    Username = usuariosEvento.Username,
                    CodGrupo = usuariosEvento.CodGrupo,
                    CodEvento = usuariosEvento.CodEvento,
                    Aceptar = usuariosEvento.Aceptar
                };

                // Agregar el nuevo objeto UsuariosEvento a la base de datos
                _context.UsuariosEventos.Add(nuevoUsuariosEvento);
                _context.SaveChanges();

                return Ok(new { message = "Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        // DELETE: api/UsuariosEventoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuariosEvento(int id)
        {
            if (_context.UsuariosEventos == null)
            {
                return NotFound();
            }
            var usuariosEvento = await _context.UsuariosEventos.FindAsync(id);
            if (usuariosEvento == null)
            {
                return NotFound();
            }

            _context.UsuariosEventos.Remove(usuariosEvento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuariosEventoExists(int id)
        {
            return (_context.UsuariosEventos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
