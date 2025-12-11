namespace ClinicaVeterinaria.Models
{
    public class ClienteCompleto
    {
        public string NombreUsuario { get; set; } = "";
        public string Contrasena { get; set; } = "";

        public string Nombres { get; set; } = "";
        public string ApellidoPaterno { get; set; } = "";
        public string ApellidoMaterno { get; set; } = "";
        public string TipoDocumento { get; set; } = "DNI";
        public string NumeroDocumento { get; set; } = "";
        public string? Celular { get; set; }
        public string? Direccion { get; set; }
        public bool Estado { get; set; } = true;
    }
}
