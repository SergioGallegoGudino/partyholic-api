using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using partyholic_api.Models;

namespace partyholic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposLogrosController : ControllerBase
    {
        private readonly partyholicContext _context;

        public GruposLogrosController(partyholicContext context)
        {
            _context = context;
        }

        // GET: api/Logros
        [HttpGet("Logros")]
        public async Task<ActionResult<IEnumerable<Logro>>> GetLogros()
        {
            var logros = await _context.Logros.ToListAsync();
            return Ok(logros);
        }

        // GET: api/GruposLogros/Logros/5
        [HttpGet("Logros/{codGrupo}")]
        public async Task<ActionResult<IEnumerable<GruposLogro>>> GetLogrosPorGrupo(int codGrupo)
        {
            var logrosGrp = _context.GruposLogros.Where(a => a.CodGrupo == codGrupo);
            return Ok(logrosGrp);
        }


        // GET: api/GruposLogroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GruposLogro>>> GetGruposLogros()
        {
          if (_context.GruposLogros == null)
          {
              return NotFound();
          }
            return await _context.GruposLogros.ToListAsync();
        }

        // GET: api/GruposLogroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GruposLogro>> GetGruposLogro(int id)
        {
          if (_context.GruposLogros == null)
          {
              return NotFound();
          }
            var gruposLogro = await _context.GruposLogros.FindAsync(id);

            if (gruposLogro == null)
            {
                return NotFound();
            }

            return gruposLogro;
        }

        // GET: api/GruposLogroes/{cod_grupo}
        [HttpGet("CodGrupo/{cod_grupo}")]
        public async Task<ActionResult<IEnumerable<GruposLogro>>> GetGrpLogro(int cod_grupo)
        {

            var logrosGrp = _context.GruposLogros.Where(a => a.CodGrupo == cod_grupo);

            if (logrosGrp.Count() == 0)
            {
                return NotFound();
            }

            return await logrosGrp.ToListAsync();
        }

        // PUT: api/GruposLogroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGruposLogro(int id, GruposLogro gruposLogro)
        {
            if (id != gruposLogro.Id)
            {
                return BadRequest();
            }

            _context.Entry(gruposLogro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GruposLogroExists(id))
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

        // POST: api/GruposLogroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GruposLogro>> CrearGrupoConLogros(Grupo grupo)
        {
            var grupoExistente = await _context.Grupos.FindAsync(grupo.CodGrupo);
            if (grupoExistente == null)
            {
                return NotFound("El grupo especificado no existe.");
            }

            List<Logro> logros = await _context.Logros.ToListAsync();

            // Crea las entradas en la tabla GruposLogros
            foreach (var logro in logros)
            {
                GruposLogro grupoLogro = new GruposLogro
                {
                    CodGrupo = grupoExistente.CodGrupo,
                    CodLogro = logro.CodLogro,
                    Alcanzado = false,
                    Actual = 0
                };
                _context.GruposLogros.Add(grupoLogro);
            }
            await _context.SaveChangesAsync();

            return Ok();
        }


        // DELETE: api/GruposLogroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGruposLogro(int id)
        {
            if (_context.GruposLogros == null)
            {
                return NotFound();
            }
            var gruposLogro = await _context.GruposLogros.FindAsync(id);
            if (gruposLogro == null)
            {
                return NotFound();
            }

            _context.GruposLogros.Remove(gruposLogro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GruposLogroExists(int id)
        {
            return (_context.GruposLogros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
