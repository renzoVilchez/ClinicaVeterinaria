namespace ClinicaVeterinaria.Models
{
    public class HorariosVeterinario
    {
        public int IdHorario { get; set; }
        public int IdVeterinario { get; set; }
        public string? NombreVeterinario { get; set; }   
        public string? DiaSemana { get; set; }
        public int HoraInicio { get; set; }
        public int HoraFin { get; set; }
        public bool Disponible { get; set; }
    }
}
