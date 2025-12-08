namespace ClinicaVeterinaria.Models
{
    public class Servicio
    {
        public int IdServicio { get; set; }
        public string? NombreServicio { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
    }
}
