using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using partyholic_api.Models;

namespace partyholic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosGruposController : ControllerBase
    {
        private readonly partyholicContext _context;

        public UsuariosGruposController(partyholicContext context)
        {
            _context = context;
        }

        // GET: api/UsuariosGrupoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuariosGrupo>>> GetUsuariosGrupos()
        {
          if (_context.UsuariosGrupos == null)
          {
              return NotFound();
          }
            return await _context.UsuariosGrupos.ToListAsync();
        }

        // GET: api/UsuariosGrupoes/getGruposUsuario/username
        [HttpGet("getGruposUsuario/{username}")]
        public async Task<ActionResult<IEnumerable<Grupo>>> GetGruposUsuario(string username)
        {
            var gruposUsuario = await _context.UsuariosGrupos
                .Include(ug => ug.CodGrupoNavigation)
                .Where(ug => ug.Username == username)
                .Select(ug => ug.CodGrupoNavigation)
                .ToListAsync();

            if (gruposUsuario == null || !gruposUsuario.Any())
            {
                return Ok();
            }

            return Ok(gruposUsuario);
        }

        [HttpGet("getUsuarioGrupo/{username}/{codGrupo}")]
        public async Task<ActionResult<IEnumerable<Grupo>>> GetUsuarioGrupo(string username, int codGrupo)
        {
            var usuarioGrupo = await _context.UsuariosGrupos
                .Where(ug => ug.Username == username && ug.CodGrupo == codGrupo)
                .ToListAsync();

            if (usuarioGrupo == null || !usuarioGrupo.Any())
            {
                return Ok();
            }

            return Ok(usuarioGrupo);
        }


        // GET: api/UsuariosGrupoes/{cod_grupo}
        [HttpGet("CodGrupo/{cod_grupo}")]
        public async Task<ActionResult<IEnumerable<UsuariosGrupo>>> GetUsuariosGrupoGrupo(int cod_grupo)
        {

            var eventoGrupos = _context.UsuariosGrupos.Where(a => a.CodGrupo == cod_grupo);

            if (eventoGrupos.Count() == 0)
            {
                return NotFound();
            }

            return await eventoGrupos.ToListAsync();
        }
        //-----------------------

        // PUT: api/UsuariosGrupoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuariosGrupo(int id, UsuariosGrupo usuariosGrupo)
        {
            if (id != usuariosGrupo.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuariosGrupo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosGrupoExists(id))
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

        [HttpPut("{codGrupo}/{username}")]
        public async Task<IActionResult> PutAdmin(int codGrupo, string username, UsuariosGrupo usuario)
        {

            var usuarioGrupo = await _context.UsuariosGrupos.Where(a => a.CodGrupo == codGrupo && a.Username == username).FirstOrDefaultAsync();

            if (usuarioGrupo == null)
            {
                return NotFound();
            }

            usuarioGrupo.EsAdmin = usuario.EsAdmin;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/UsuariosGrupoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuariosGrupo>> PostUsuariosGrupo(UsuariosGrupo usuariosGrupo)
        {
          if (_context.UsuariosGrupos == null)
          {
              return Problem("Entity set 'PartyholicContext.UsuariosGrupos'  is null.");
          }
            _context.UsuariosGrupos.Add(usuariosGrupo);
            await _context.SaveChangesAsync();

            return Ok(usuariosGrupo);
        }

        // GET: api/UsuariosGrupoes/getUsuarios/codgrupo

        [HttpGet("getUsuarios/{codgrupo}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetGruposLike(string codGrupo)
        {
            try
            {
                var usuariosGrupo = await _context.UsuariosGrupos
                    .Where(ug => ug.CodGrupo == int.Parse(codGrupo))
                    .Select(ug => new
                    {
                        Usuario = ug.UsernameNavigation,
                        EsAdmin = ug.EsAdmin
                    })
                    .ToListAsync();

                if (usuariosGrupo == null || !usuariosGrupo.Any())
                {
                    return Ok();
                }

                return Ok(usuariosGrupo);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar grupos: " + ex.ToString());
            }
        }


        // DELETE: api/UsuariosGrupoes/5
        [HttpDelete("{codGrupo}/{username}")]
        public async Task<IActionResult> DeleteUsuariosGrupo(string codGrupo, string username)
        {
            var usuarioGrupo = await _context.UsuariosGrupos
                .FirstOrDefaultAsync(ug => ug.Username == username && ug.CodGrupo == int.Parse(codGrupo));

            if (usuarioGrupo == null)
            {
                return NotFound();
            }

            _context.UsuariosGrupos.Remove(usuarioGrupo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuariosGrupoExists(int id)
        {
            return (_context.UsuariosGrupos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
