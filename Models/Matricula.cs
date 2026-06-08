using System;

namespace AcademicPlatformApi.Models
{
    public class Matricula
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public int HorarioId { get; set; }
        public DateTime FechaMatricula { get; set; } = DateTime.Now;

        // Propiedades de navegación relacional
        public Estudiante? Estudiante { get; set; }
        public Horario? Horario { get; set; }
    }
}
