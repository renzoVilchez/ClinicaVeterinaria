namespace ClinicaVeterinaria.Models
{
    public class Cita
    {
        public int IdCita { get; set; }
        public int IdMascota { get; set; }
        public int IdServicio { get; set; }
        public int IdVeterinario { get; set; }
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }
        public string? Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}