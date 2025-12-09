using ClinicaVeterinaria.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClinicaVeterinaria.Data
{
    public class TicketRepository
    {
        private readonly SqlConnectionFactory _factory;

        public TicketRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public int Insertar(Ticket t)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_InsertarTicket", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idCita", t.IdCita);
            cmd.Parameters.AddWithValue("@montoTotal", t.MontoTotal);
            cmd.Parameters.AddWithValue("@metodoPago", t.MetodoPago ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@estado", t.Estado ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@fechaPago", t.FechaPago ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@observaciones", t.Observaciones ?? (object)DBNull.Value);

            con.Open();

            var result = cmd.ExecuteScalar();
            return result == null ? 0 : Convert.ToInt32(result);
        }

        public int Actualizar(Ticket t)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ActualizarTicket", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idTicket", t.IdTicket);
            cmd.Parameters.AddWithValue("@idCita", t.IdCita);
            cmd.Parameters.AddWithValue("@montoTotal", t.MontoTotal);
            cmd.Parameters.AddWithValue("@metodoPago", t.MetodoPago ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@estado", t.Estado ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@fechaPago", t.FechaPago ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@observaciones", t.Observaciones ?? (object)DBNull.Value);

            con.Open();

            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }

        public int Eliminar(int idTicket)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_EliminarTicket", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idTicket", idTicket);

            con.Open();

            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }

        public Ticket ObtenerPorId(int idTicket)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ObtenerTicketPorId", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idTicket", idTicket);

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (!dr.Read()) return null;

            return new Ticket
            {
                IdTicket = Convert.ToInt32(dr["idTicket"]),
                IdCita = Convert.ToInt32(dr["idCita"]),
                CodigoTicket = dr["codigoTicket"].ToString(),
                FechaEmision = Convert.ToDateTime(dr["fechaEmision"]),
                MontoTotal = Convert.ToDecimal(dr["montoTotal"]),
                MetodoPago = dr["metodoPago"].ToString(),
                Estado = dr["estado"].ToString(),
                FechaPago = dr["fechaPago"] == DBNull.Value ? null : Convert.ToDateTime(dr["fechaPago"]),
                Observaciones = dr["observaciones"].ToString()
            };
        }

        public List<Ticket> ListarTodos()
        {
            var lista = new List<Ticket>();

            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarTickets", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Ticket
                {
                    IdTicket = Convert.ToInt32(dr["idTicket"]),
                    IdCita = Convert.ToInt32(dr["idCita"]),
                    CodigoTicket = dr["codigoTicket"].ToString(),
                    FechaEmision = Convert.ToDateTime(dr["fechaEmision"]),
                    MontoTotal = Convert.ToDecimal(dr["montoTotal"]),
                    MetodoPago = dr["metodoPago"].ToString(),
                    Estado = dr["estado"].ToString(),
                    FechaPago = dr["fechaPago"] == DBNull.Value ? null : Convert.ToDateTime(dr["fechaPago"]),
                    Observaciones = dr["observaciones"].ToString()
                });
            }

            return lista;
        }
    }
}
