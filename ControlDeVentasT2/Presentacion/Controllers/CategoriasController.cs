using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Alamcen;
using Presentacion.Models.Almacen.Categoria;
using Presentacion.Models.Almacen.Articulos;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly DBContextSistema _context;

        public CategoriasController(DBContextSistema context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
          if (_context.Categorias == null)
          {
              return NotFound();
          }
            return await _context.Categorias.ToListAsync();
        }

        // METODO LISTAR //**********************************************************************************
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoriaViewModel>> ListarCategorias()
        {
            var categoria = await _context.Categorias.ToListAsync();
            return categoria.Select(c => new CategoriaViewModel
            {
                IdCategoria = c.IdCategoria,
                NombreCategoria = c.NombreCategoria,
                Descripcion = c.Descripcion,
                Estado = c.Estado
            });
        }


        // METODO SELECCIONAR CATEGORIA  //**********************************************************************************
        [HttpGet("[action]")]
        public async Task<IEnumerable<SeleccionarArticuloViewModel>> SeleccionarCategorias()
        {
            var categoria = await _context.Categorias.Where(a => a.Estado == true).ToArrayAsync();
            return categoria.Select(c => new SeleccionarArticuloViewModel
            {
                IdCategoria = c.IdCategoria,
                NombreCategoria = c.NombreCategoria,
            });
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
          if (_context.Categorias == null)
          {
              return NotFound();
          }
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.IdCategoria)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        // MODIFICAR CATEGORIA////**********************************************************************************
        [HttpPut("[action]")]
        public async Task<IActionResult> ModificarCategorias([FromBody] ModificarViewModel modelcategoria)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (modelcategoria.IdCategoria<0)
            {
                return BadRequest(ModelState);
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == modelcategoria.IdCategoria);

            if(categoria == null)
            {
                return NotFound();
            }

            categoria.NombreCategoria = modelcategoria.NombreCategoria;
            categoria.Descripcion = modelcategoria.Descripcion;

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

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
          if (_context.Categorias == null)
          {
              return Problem("Entity set 'DBContextSistema.Categorias'  is null.");
          }
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoria", new { id = categoria.IdCategoria }, categoria);
        }

        // METODO INSERTAR CATEGORIA//**********************************************************************************
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarCategoria(InsertarViewModel modelCategorias)
        {
            if (_context.Categorias == null)
            {
                return Problem("Entity set 'DBContextSistema.Categorias'  is null.");
            }

            Categoria categoria = new Categoria
            {
                NombreCategoria = modelCategorias.NombreCategoria,
                Descripcion = modelCategorias.Descripcion,
                Estado = true
            };
            _context.Categorias.Add(categoria);
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



        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            if (_context.Categorias == null)
            {
                return NotFound();
            }
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // BORRAR CATEGORIA//**********************************************************************************
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> BorrarCategoria(int id)
        {
            if (_context.Categorias == null)
            {
                return NotFound();
            }
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        //DESACTIVAR CATEGORIA//**********************************************************************************
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> DesactivarCategoria([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == id);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.Estado = false;

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
            public async Task<IActionResult> ActivarCategoria([FromRoute] int id)
            {
                if (id < 0)
                {
                    return BadRequest();
                }

                var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == id);

                if (categoria == null)
                {
                    return NotFound();
                }

                categoria.Estado = true;

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


            private bool CategoriaExists(int id)
        {
            return (_context.Categorias?.Any(e => e.IdCategoria == id)).GetValueOrDefault();
        }


    }
}
