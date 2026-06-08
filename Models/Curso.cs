namespace AcademicPlatformApi.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Creditos { get; set; }
        public string Ciclo { get; set; } = string.Empty;
    }
}
