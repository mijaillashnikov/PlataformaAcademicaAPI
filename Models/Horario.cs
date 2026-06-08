using System;

namespace AcademicPlatformApi.Models
{
    public class Horario
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public int DocenteId { get; set; }
        public string DiaSemana { get; set; } = string.Empty;
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Aula { get; set; } = string.Empty;

        // Propiedades de navegación relacional
        public Curso? Curso { get; set; }
        public Docente? Docente { get; set; }
    }
} 