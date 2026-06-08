using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademicPlatformApi.Data;
using AcademicPlatformApi.Models;

namespace AcademicPlatformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocentesController : ControllerBase
    {
        private readonly AcademicoDbContext _context;

        public DocentesController(AcademicoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Docente>>> GetDocentes()
        {
            return await _context.Docentes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Docente>> GetDocente(int id)
        {
            var docente = await _context.Docentes.FindAsync(id);
            if (docente == null) return NotFound();
            return docente;
        }

        [HttpPost]
        public async Task<ActionResult<Docente>> PostDocente(Docente docente)
        {
            _context.Docentes.Add(docente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDocente), new { id = docente.Id }, docente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocente(int id, Docente docente)
        {
            if (id != docente.Id) return BadRequest();
            _context.Entry(docente).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocenteExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocente(int id)
        {
            var docente = await _context.Docentes.FindAsync(id);
            if (docente == null) return NotFound();

            _context.Docentes.Remove(docente);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool DocenteExists(int id) => _context.Docentes.Any(e => e.Id == id);
    }
}