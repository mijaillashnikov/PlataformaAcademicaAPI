using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademicPlatformApi.Data;
using AcademicPlatformApi.Models;

namespace AcademicPlatformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculasController : ControllerBase
    {
        private readonly AcademicoDbContext _context;

        public MatriculasController(AcademicoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matricula>>> GetMatriculas()
        {
            return await _context.Matriculas
                                 .Include(m => m.Estudiante)
                                 .Include(m => m.Horario)
                                 .ThenInclude(h => h!.Curso)
                                 .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Matricula>> GetMatricula(int id)
        {
            var matricula = await _context.Matriculas
                                          .Include(m => m.Estudiante)
                                          .Include(m => m.Horario)
                                          .ThenInclude(h => h!.Curso)
                                          .FirstOrDefaultAsync(m => m.Id == id);

            if (matricula == null) return NotFound();
            return matricula;
        }

        [HttpPost]
        public async Task<ActionResult<Matricula>> PostMatricula(Matricula matricula)
        {
            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMatricula), new { id = matricula.Id }, matricula);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatricula(int id, Matricula matricula)
        {
            if (id != matricula.Id) return BadRequest();
            _context.Entry(matricula).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatriculaExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatricula(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null) return NotFound();

            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool MatriculaExists(int id) => _context.Matriculas.Any(e => e.Id == id);
    }
}