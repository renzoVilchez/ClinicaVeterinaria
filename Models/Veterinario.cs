namespace ClinicaVeterinaria.Models
{
    public class Veterinario
    {
        public int IdVeterinario { get; set; }
        public int IdUsuario { get; set; }
        public string? Especialidad { get; set; }
        public bool Estado { get; set; }
    }
}
