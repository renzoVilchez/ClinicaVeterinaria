using System.Data;
using ClinicaVeterinaria.Models;
using Microsoft.Data.SqlClient;

namespace ClinicaVeterinaria.Data
{
    public class ChatBotRepository
    {
        private readonly SqlConnectionFactory _factory;

        public ChatBotRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        // ===========================
        // LISTAR TODO
        // ===========================
        public List<ChatBot> ListarTodos()
        {
            var lista = new List<ChatBot>();

            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarChatBot", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new ChatBot
                {
                    IdPregunta = Convert.ToInt32(dr["idPregunta"]),
                    Pregunta = dr["pregunta"].ToString(),
                    Respuesta = dr["respuesta"].ToString()
                });
            }

            return lista;
        }

        // ===========================
        // OBTENER POR ID
        // ===========================
        public ChatBot? ObtenerPorId(int id)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ObtenerChatBotPorId", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idPregunta", id);

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (!dr.Read()) return null;

            return new ChatBot
            {
                IdPregunta = Convert.ToInt32(dr["idPregunta"]),
                Pregunta = dr["pregunta"].ToString(),
                Respuesta = dr["respuesta"].ToString()
            };
        }

        // ===========================
        // INSERTAR
        // ===========================
        public int Insertar(ChatBot c)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_InsertarChatBot", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@pregunta", (object?)c.Pregunta ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@respuesta", (object?)c.Respuesta ?? DBNull.Value);

            con.Open();
            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        // ===========================
        // ACTUALIZAR
        // ===========================
        public int Actualizar(ChatBot c)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ActualizarChatBot", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idPregunta", c.IdPregunta);
            cmd.Parameters.AddWithValue("@pregunta", (object?)c.Pregunta ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@respuesta", (object?)c.Respuesta ?? DBNull.Value);

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }

        // ===========================
        // ELIMINAR
        // ===========================
        public int Eliminar(int id)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_EliminarChatBot", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idPregunta", id);

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }
    }
}
