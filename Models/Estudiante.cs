namespace AcademicPlatformApi.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string CodigoUniversitario { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
    }
}
