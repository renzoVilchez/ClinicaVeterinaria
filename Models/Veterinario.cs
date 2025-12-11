public class Veterinario
{
    public int IdVeterinario { get; set; }
    public int IdUsuario { get; set; }
    public string? Especialidad { get; set; }
    public bool Estado { get; set; }

    public string? Nombres { get; set; }
    public string? ApellidoPaterno { get; set; }
    public string? ApellidoMaterno { get; set; }
    public string? TipoDocumento { get; set; }
    public string? NumeroDocumento { get; set; }
    public string? Celular { get; set; }
    public string? Direccion { get; set; }
}
