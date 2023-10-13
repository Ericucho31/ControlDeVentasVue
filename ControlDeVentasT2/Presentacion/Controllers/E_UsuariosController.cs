using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Usuarios;
using Entidades.Alamcen;
using Presentacion.Models.Almacen.Articulos;
using Presentacion.Models.Usuario.Usuarios;
using Microsoft.AspNetCore.Identity;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class E_UsuariosController : ControllerBase
    {
        private readonly DBContextSistema _context;

        public E_UsuariosController(DBContextSistema context)
        {
            _context = context;
        }

        // METODO LISTAR //**********************************************************************************
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioViewModel>> ListarUsuarios()
        {
            var usuarios = await _context.Usuarios.Include(a => a.IdRolNavigation).ToListAsync();
            return usuarios.Select(u => new UsuarioViewModel
            {
                IdUsuario = u.IdUsuario,
                IdRol = u.IdRol,
                NombreUsuario = u.NombreUsuario,
                TipoDocumento = u.TipoDocumento,
                NumeroDocumento = u.NumeroDocumento,
                Direccion = u.Direccion,
                Telefono = u.Telefono,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                Estado = u.Estado,

                Rol = u.IdRolNavigation.NombreRol
            }); ;
        }

        // GET: api/E_Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<E_Usuarios>>> GetUsuarios()
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/E_Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<E_Usuarios>> GetE_Usuarios(int id)
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            var e_Usuarios = await _context.Usuarios.FindAsync(id);

            if (e_Usuarios == null)
            {
                return NotFound();
            }

            return e_Usuarios;
        }

        // METODO MODIFICAR USUARIO//**********************************************************************************
        [HttpPut("[action]")]
        public async Task<IActionResult> ModificarUsuarios([FromBody]ModificarUsuarioViewModel modelUsuario)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'DBContextSistema.Usuarios'  is null.");
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == modelUsuario.IdUsuario);

            

            var email = modelUsuario.Email.ToUpper();
            if (await _context.Usuarios.AnyAsync(u => u.Email == email && u.IdUsuario != modelUsuario.IdUsuario))
            {
                return BadRequest("El Email de este usuario ya existe"); //Función para validar que no se repita un Email
            }
            if (usuario == null)
            {
                return NotFound(); //Función para validar que no se repita un Email
            }


            usuario.IdRol = modelUsuario.IdRol;
            usuario.NombreUsuario = modelUsuario.NombreUsuario;
            usuario.TipoDocumento = modelUsuario.TipoDocumento;
            usuario.NumeroDocumento = modelUsuario.NumeroDocumento;
            usuario.Direccion = modelUsuario.Direccion;
            usuario.Telefono = modelUsuario.Telefono;
            usuario.Email = modelUsuario.Email;

            if(modelUsuario.ActualizarPassword == true)
            {
                CreaPassword(modelUsuario.Password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.PasswordHash = passwordHash;
                usuario.PasswordSalt = passwordSalt;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                string Error = e.Message;
                var inner = e.InnerException;
                return BadRequest();
            }
            return Ok();
        }

        // PUT: api/E_Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutE_Usuarios(int id, E_Usuarios e_Usuarios)
        {
            if (id != e_Usuarios.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(e_Usuarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!E_UsuariosExists(id))
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

        // POST: api/E_Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<E_Usuarios>> PostE_Usuarios(E_Usuarios e_Usuarios)
        {
          if (_context.Usuarios == null)
          {
              return Problem("Entity set 'DBContextSistema.Usuarios'  is null.");
          }
            _context.Usuarios.Add(e_Usuarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetE_Usuarios", new { id = e_Usuarios.IdUsuario }, e_Usuarios);
        }

        // METODO CREAR PASSWORD//**********************************************************************************
        public static void CreaPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        // METODO INSERTAR USUARIO//**********************************************************************************
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarUsuarios(InsertarUsuarioViewModel modelUsuario)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'DBContextSistema.Usuarios'  is null.");
            }

            CreaPassword(modelUsuario.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var email = modelUsuario.Email.ToUpper();
            if (await _context.Usuarios.AnyAsync(u => u.Email == email))
            {
                return BadRequest("El Email de este usuario ya existe"); //Función para validar que no se repita un Email
            }
            E_Usuarios usuario = new E_Usuarios
            {
                IdRol = modelUsuario.IdRol,
                NombreUsuario = modelUsuario.NombreUsuario,
                TipoDocumento = modelUsuario.TipoDocumento,
                NumeroDocumento = modelUsuario.NumeroDocumento,
                Direccion = modelUsuario.Direccion,
                Telefono = modelUsuario.Telefono,
                Email = modelUsuario.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Estado = modelUsuario.Estado
            };
            _context.Usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception e) 
            {
                string Error = e.Message;
                var inner = e.InnerException;
                return BadRequest();
            }
            return Ok();
        }

        // DELETE: api/E_Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteE_Usuarios(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var e_Usuarios = await _context.Usuarios.FindAsync(id);
            if (e_Usuarios == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(e_Usuarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //DESACTIVAR CATEGORIA//**********************************************************************************
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> DesactivarUsuarios([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Estado = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        //ACTIVAR CATEGORIA//**********************************************************************************
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ActivarUsuarios([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Estado = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }



        private bool E_UsuariosExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
