namespace ClinicaVeterinaria.Models
{
    public class Mascota
    {
        public int IdMascota { get; set; }
        public int IdCliente { get; set; }
        public int IdEspecie { get; set; }
        public string? Nombre { get; set; }
        public string? Raza { get; set; }
        public int? Edad { get; set; }
        public decimal? Peso { get; set; }
        public string? Sexo { get; set; }
        public bool Estado { get; set; }
    }
}
