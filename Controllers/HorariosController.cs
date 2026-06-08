using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademicPlatformApi.Data;
using AcademicPlatformApi.Models;

namespace AcademicPlatformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly AcademicoDbContext _context;

        public HorariosController(AcademicoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Horario>>> GetHorarios()
        {
            return await _context.Horarios
                                 .Include(h => h.Curso)
                                 .Include(h => h.Docente)
                                 .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Horario>> GetHorario(int id)
        {
            var horario = await _context.Horarios
                                        .Include(h => h.Curso)
                                        .Include(h => h.Docente)
                                        .FirstOrDefaultAsync(h => h.Id == id);

            if (horario == null) return NotFound();
            return horario;
        }

        [HttpPost]
        public async Task<ActionResult<Horario>> PostHorario(Horario horario)
        {
            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHorario), new { id = horario.Id }, horario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorario(int id, Horario horario)
        {
            if (id != horario.Id) return BadRequest();
            _context.Entry(horario).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorarioExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null) return NotFound();

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool HorarioExists(int id) => _context.Horarios.Any(e => e.Id == id);
    }
}