using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Usuarios;
using Presentación.Models.Usuario.Personas;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly DBContextSistema _context;

        public PersonasController(DBContextSistema context)
        {
            _context = context;
        }

        #region Insertar. POST: api/Personas/InsertarPersona
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarPersona(PersonaViewModel modelPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (_context.Personas == null)
            {
                return Problem("Entity set 'Context.persona is null");
            }
            var email = modelPersona.EmailPersona.ToUpper();
            if (await _context.Personas.AnyAsync(p => p.EmailPersona == email))
            {
                return BadRequest("El Email de la persona ya existe");
            }
            Persona persona = new()
            {
                TipoPersona = modelPersona.TipoPersona,
                NombrePersona = modelPersona.NombrePersona,
                Tipodocumento = modelPersona.Tipodocumento,
                NumeroDocumento = modelPersona.NumeroDocumento,
                DireccionPersona = modelPersona.DireccionPersona,
                TelefonoPersona = modelPersona.TelefonoPersona,
                EmailPersona = modelPersona.EmailPersona.ToUpper(),
            };
            _context.Personas.Add(persona);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                var inner = ex.InnerException;
                return BadRequest();
            }
            return Ok();
        }
        #endregion

        #region Modificar. PUT: api/Personas/ModificarPersona
        [HttpPut("[action]")]
        public async Task<IActionResult> ModificarPersona(ModificarPersonaViewModel modelPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (modelPersona.IdPersona <= 0)
            {
                return Problem("Entity set 'Context.persona is null");
            }
            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.IdPersona == modelPersona.IdPersona);
            var email = modelPersona.EmailPersona.ToUpper();
            if (await _context.Personas.AnyAsync(p => p.EmailPersona == email && p.IdPersona != modelPersona.IdPersona))
            {
                return BadRequest("El Email de la persona ya existe");
            }
            if (persona == null)
            {
                return NotFound();
            }

            persona.IdPersona = modelPersona.IdPersona;
            persona.TipoPersona = modelPersona.TipoPersona;
            persona.NombrePersona = modelPersona.NombrePersona;
            persona.Tipodocumento = modelPersona.Tipodocumento;
            persona.NumeroDocumento = modelPersona.NumeroDocumento;
            persona.DireccionPersona = modelPersona.DireccionPersona;
            persona.TelefonoPersona = modelPersona.TelefonoPersona;
            persona.EmailPersona = modelPersona.EmailPersona.ToUpper();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                var inner = ex.InnerException;
                return BadRequest();
            }
            return Ok();
        }
        #endregion

        #region Listar. GET: api/Personas/ListarClientes
        [HttpGet("[action]")]
        public async Task<IEnumerable<ModificarPersonaViewModel>> ListarCliente()
        {
            var persona = await _context.Personas.Where(p => p.TipoPersona == "Cliente").ToListAsync();
            return persona.Select(p => new ModificarPersonaViewModel
            {
                IdPersona = p.IdPersona,
                TipoPersona = p.TipoPersona,
                NombrePersona = p.NombrePersona,
                Tipodocumento = p.Tipodocumento,
                NumeroDocumento = p.NumeroDocumento,
                DireccionPersona = p.DireccionPersona,
                TelefonoPersona = p.TelefonoPersona,
                EmailPersona = p.EmailPersona
            });

        }
        #endregion
        #region Listar. GET: api/Personas/ListarProvedores
        [HttpGet("[action]")]
        public async Task<IEnumerable<ModificarPersonaViewModel>> ListarProvedores()
        {
            var persona = await _context.Personas.Where(p => p.TipoPersona == "Provedor").ToListAsync();
            return persona.Select(p => new ModificarPersonaViewModel
            {
                IdPersona = p.IdPersona,
                TipoPersona = p.TipoPersona,
                NombrePersona = p.NombrePersona,
                Tipodocumento = p.Tipodocumento,
                NumeroDocumento = p.NumeroDocumento,
                DireccionPersona = p.DireccionPersona,
                TelefonoPersona = p.TelefonoPersona,
                EmailPersona = p.EmailPersona
            });

        }
        #endregion



        // GET: api/Personas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
        {
          if (_context.Personas == null)
          {
              return NotFound();
          }
            return await _context.Personas.ToListAsync();
        }

        // GET: api/Personas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
          if (_context.Personas == null)
          {
              return NotFound();
          }
            var persona = await _context.Personas.FindAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }

        // PUT: api/Personas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, Persona persona)
        {
            if (id != persona.IdPersona)
            {
                return BadRequest();
            }

            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
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

        // POST: api/Personas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(Persona persona)
        {
          if (_context.Personas == null)
          {
              return Problem("Entity set 'DBContextSistema.Personas'  is null.");
          }
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersona", new { id = persona.IdPersona }, persona);
        }

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            if (_context.Personas == null)
            {
                return NotFound();
            }
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaExists(int id)
        {
            return (_context.Personas?.Any(e => e.IdPersona == id)).GetValueOrDefault();
        }
    }
}
