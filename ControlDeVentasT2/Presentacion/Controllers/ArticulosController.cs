using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Alamcen;
using Presentacion.Models.Usuario.Roles;
using Presentacion.Models.Almacen.Articulos;
using Entidades.Usuarios;
using System.Collections;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly DBContextSistema _context;

        public ArticulosController(DBContextSistema context)
        {
            _context = context;
        }


        // METODO LISTAR //**********************************************************************************
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticulosViewModel>> ListarArticulos()
        {
            var articulos = await _context.Articulos.Include(a => a.IdCategoriaNavigation).ToListAsync();
            return articulos.Select(a => new ArticulosViewModel
            {
                IdArticulo = a.IdArticulo,
                IdCategoria = a.IdCategoria,
                Categoria = a.IdCategoriaNavigation.NombreCategoria,
                CodigoArticulo = a.CodigoArticulo,
                NombreArticulo = a.NombreArticulo,
                PrecioVenta = a.PrecioVenta,
                Stock = a.Stock,
                DescripcionArticulo = a.DescripcionArticulo,
                Estado = a.Estado


            });
        }

        // METODO OBTENER (ID) //**********************************************************************************
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ObtenerArticulo([FromRoute] int id)
        {
            var articulo = await _context.Articulos.Include(a => a.IdCategoriaNavigation).SingleOrDefaultAsync(a => a.IdArticulo == id);

            if(articulo == null)
            {
                return NotFound();
            }

            return Ok(new ArticulosViewModel
            {
                IdArticulo = articulo.IdArticulo,
                IdCategoria = articulo.IdCategoria,
                CodigoArticulo = articulo.CodigoArticulo,
                NombreArticulo = articulo.NombreArticulo,
                PrecioVenta = articulo.PrecioVenta,
                Stock = articulo.Stock,
                DescripcionArticulo = articulo.DescripcionArticulo,
                Estado = articulo.Estado,
                Categoria = articulo.IdCategoriaNavigation.NombreCategoria
            });
        }


        // GET: api/Articulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Articulos>>> GetArticulos()
        {
          if (_context.Articulos == null)
          {
              return NotFound();
          }
            return await _context.Articulos.ToListAsync();
        }

        // GET: api/Articulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Articulos>> GetArticulos(int id)
        {
          if (_context.Articulos == null)
          {
              return NotFound();
          }
            var articulos = await _context.Articulos.FindAsync(id);

            if (articulos == null)
            {
                return NotFound();
            }

            return articulos;
        }


        // MODIFICAR ROL////**********************************************************************************
        [HttpPut("[action]")]
        public async Task<IActionResult> ModificarArticulos([FromBody] ModificarArticuloViewModel modelArticulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (modelArticulo.IdArticulo < 0)
            {
                return BadRequest(ModelState);
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.IdArticulo == modelArticulo.IdArticulo);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.IdCategoria = modelArticulo.IdCategoria;
            articulo.CodigoArticulo = modelArticulo.CodigoArticulo;
            articulo.NombreArticulo = modelArticulo.NombreArticulo;
            articulo.PrecioVenta = modelArticulo.PrecioVenta;
            articulo.Stock = modelArticulo.Stock;
            articulo.DescripcionArticulo = modelArticulo.DescripcionArticulo;
            articulo.Estado = modelArticulo.Estado;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Articulos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticulos(int id, Articulos articulos)
        {
            if (id != articulos.IdArticulo)
            {
                return BadRequest();
            }

            _context.Entry(articulos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticulosExists(id))
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

        // METODO INSERTAR ARTICULO//**********************************************************************************
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarArticulos(InsertarArticuloViewModel modelArticulo)
        {
            if (_context.Articulos == null)
            {
                return Problem("Entity set 'DBContextSistema.Roles'  is null.");
            }

            Articulos articulo = new Articulos
            {
                IdCategoria = modelArticulo.IdCategoria,
                CodigoArticulo = modelArticulo.CodigoArticulo,
                NombreArticulo = modelArticulo.NombreArticulo,
                PrecioVenta = modelArticulo.PrecioVenta,
                Stock = modelArticulo.Stock,
                DescripcionArticulo = modelArticulo.DescripcionArticulo,
                Estado = modelArticulo.Estado
            };
            _context.Articulos.Add(articulo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        // POST: api/Articulos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Articulos>> PostArticulos(Articulos articulos)
        {
          if (_context.Articulos == null)
          {
              return Problem("Entity set 'DBContextSistema.Articulos'  is null.");
          }
            _context.Articulos.Add(articulos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticulos", new { id = articulos.IdArticulo }, articulos);
        }

        // DELETE: api/Articulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticulos(int id)
        {
            if (_context.Articulos == null)
            {
                return NotFound();
            }
            var articulos = await _context.Articulos.FindAsync(id);
            if (articulos == null)
            {
                return NotFound();
            }

            _context.Articulos.Remove(articulos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //DESACTIVAR CATEGORIA//**********************************************************************************
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> DesactivarArticulos([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.IdArticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.Estado = false;

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
        public async Task<IActionResult> ActivarArticulos([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.IdArticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.Estado = true;

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

        private bool ArticulosExists(int id)
        {
            return (_context.Articulos?.Any(e => e.IdArticulo == id)).GetValueOrDefault();
        }
    }
}
