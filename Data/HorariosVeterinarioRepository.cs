using ClinicaVeterinaria.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClinicaVeterinaria.Data
{
    public class HorariosVeterinarioRepository
    {
        private readonly SqlConnectionFactory _factory;

        public HorariosVeterinarioRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public int Insertar(HorariosVeterinario h)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_InsertarHorarioVeterinario", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idVeterinario", h.IdVeterinario);
            cmd.Parameters.AddWithValue("@diaSemana",
                string.IsNullOrEmpty(h.DiaSemana) ? (object)DBNull.Value : h.DiaSemana);
            cmd.Parameters.AddWithValue("@horaInicio", h.HoraInicio);
            cmd.Parameters.AddWithValue("@horaFin", h.HoraFin);
            cmd.Parameters.AddWithValue("@disponible", h.Disponible);

            con.Open();

            var result = cmd.ExecuteScalar();
            return result == null ? 0 : Convert.ToInt32(result);
        }

        public int Actualizar(HorariosVeterinario h)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ActualizarHorarioVeterinario", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idHorario", h.IdHorario);
            cmd.Parameters.AddWithValue("@idVeterinario", h.IdVeterinario);
            cmd.Parameters.AddWithValue("@diaSemana",
                string.IsNullOrEmpty(h.DiaSemana) ? (object)DBNull.Value : h.DiaSemana);
            cmd.Parameters.AddWithValue("@horaInicio", h.HoraInicio);
            cmd.Parameters.AddWithValue("@horaFin", h.HoraFin);
            cmd.Parameters.AddWithValue("@disponible", h.Disponible);

            con.Open();

            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }

        public int Eliminar(int idHorariosVeterinario)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_EliminarHorarioVeterinario", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idHorario", idHorariosVeterinario);

            con.Open();

            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }

        public HorariosVeterinario ObtenerPorId(int idHorariosVeterinario)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ObtenerHorarioVeterinarioPorId", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idHorario", idHorariosVeterinario);

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (!dr.Read()) return null;

            return new HorariosVeterinario
            {
                IdHorario = Convert.ToInt32(dr["idHorario"]),
                IdVeterinario = Convert.ToInt32(dr["idVeterinario"]),
                DiaSemana = dr["diaSemana"] == DBNull.Value ? null : dr["diaSemana"].ToString(),
                HoraInicio = Convert.ToInt32(dr["horaInicio"]),
                HoraFin = Convert.ToInt32(dr["horaFin"]),
                Disponible = Convert.ToBoolean(dr["disponible"])
            };
        }

        public List<HorariosVeterinario> ListarTodos()
        {
            var lista = new List<HorariosVeterinario>();

            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarHorariosVeterinarios", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new HorariosVeterinario
                {
                    IdHorario = Convert.ToInt32(dr["idHorario"]),
                    IdVeterinario = Convert.ToInt32(dr["idVeterinario"]),
                    NombreVeterinario = dr["nombreVeterinario"].ToString(),
                    DiaSemana = dr["diaSemana"] == DBNull.Value ? null : dr["diaSemana"].ToString(),
                    HoraInicio = Convert.ToInt32(dr["horaInicio"]),
                    HoraFin = Convert.ToInt32(dr["horaFin"]),
                    Disponible = Convert.ToBoolean(dr["disponible"])
                });
            }

            return lista;
        }
        public List<Veterinario> ListarVeterinarios()
        {
            var lista = new List<Veterinario>();

            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarVeterinarios", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new Veterinario
                {
                    IdVeterinario = Convert.ToInt32(dr["idVeterinario"]),
                    Nombres = dr["Nombres"].ToString(),
                    ApellidoPaterno = dr["ApellidoPaterno"].ToString(),
                    ApellidoMaterno = dr["ApellidoMaterno"].ToString()
                });
            }

            return lista;
        }

    }
}
