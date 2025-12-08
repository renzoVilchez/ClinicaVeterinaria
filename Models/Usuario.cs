namespace ClinicaVeterinaria.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Contrasena { get; set; }
        public string? Rol { get; set; }
        public bool Estado { get; set; }
    }

}
