using Microsoft.EntityFrameworkCore;
using AcademicPlatformApi.Models;

namespace AcademicPlatformApi.Data
{
    public class AcademicoDbContext : DbContext
    {
        public AcademicoDbContext(DbContextOptions<AcademicoDbContext> options) : base(options) { }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
    }
}
