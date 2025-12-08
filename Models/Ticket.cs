namespace ClinicaVeterinaria.Models
{
    public class Ticket
    {
        public int IdTicket { get; set; }
        public int IdCita { get; set; }
        public string? CodigoTicket { get; set; }

        public DateTime FechaEmision { get; set; }
        public decimal MontoTotal { get; set; }

        public string? MetodoPago { get; set; } 

        public string? Estado { get; set; }
        public DateTime? FechaPago { get; set; }

        public string? Observaciones { get; set; } 
    }
}