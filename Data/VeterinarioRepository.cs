using ClinicaVeterinaria.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClinicaVeterinaria.Data
{
    public class VeterinarioRepository
    {
        private readonly SqlConnectionFactory _factory;

        public VeterinarioRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        // ========================================
        // INSERTAR
        // ========================================
        public int Insertar(Veterinario v)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_InsertarVeterinario", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idUsuario", v.IdUsuario);
            cmd.Parameters.AddWithValue("@especialidad", v.Especialidad);
            cmd.Parameters.AddWithValue("@estado", v.Estado);

            cmd.Parameters.AddWithValue("@nombres", v.Nombres);
            cmd.Parameters.AddWithValue("@apellidoPaterno", v.ApellidoPaterno);
            cmd.Parameters.AddWithValue("@apellidoMaterno", v.ApellidoMaterno);

            cmd.Parameters.AddWithValue("@tipoDocumento", v.TipoDocumento);
            cmd.Parameters.AddWithValue("@numeroDocumento", v.NumeroDocumento);
            cmd.Parameters.AddWithValue("@celular", v.Celular);
            cmd.Parameters.AddWithValue("@direccion", v.Direccion);

            con.Open();
            var result = cmd.ExecuteScalar();
            return result == null ? 0 : Convert.ToInt32(result);
        }

        // ========================================
        // ACTUALIZAR
        // ========================================
        public int Actualizar(Veterinario v)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ActualizarVeterinario", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idVeterinario", v.IdVeterinario);
            cmd.Parameters.AddWithValue("@idUsuario", v.IdUsuario);
            cmd.Parameters.AddWithValue("@especialidad", v.Especialidad);
            cmd.Parameters.AddWithValue("@estado", v.Estado);

            cmd.Parameters.AddWithValue("@nombres", v.Nombres);
            cmd.Parameters.AddWithValue("@apellidoPaterno", v.ApellidoPaterno);
            cmd.Parameters.AddWithValue("@apellidoMaterno", v.ApellidoMaterno);

            cmd.Parameters.AddWithValue("@tipoDocumento", v.TipoDocumento);
            cmd.Parameters.AddWithValue("@numeroDocumento", v.NumeroDocumento);
            cmd.Parameters.AddWithValue("@celular", v.Celular);
            cmd.Parameters.AddWithValue("@direccion", v.Direccion);

            con.Open();

            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }

        // ========================================
        // ELIMINAR
        // ========================================
        public int Eliminar(int idVeterinario)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_EliminarVeterinario", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idVeterinario", idVeterinario);

            con.Open();

            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }

        // ========================================
        // OBTENER POR ID
        // ========================================
        public Veterinario ObtenerPorId(int idVeterinario)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ObtenerVeterinarioPorId", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idVeterinario", idVeterinario);

            con.Open();

            using var dr = cmd.ExecuteReader();
            if (!dr.Read()) return null;

            return new Veterinario
            {
                IdVeterinario = Convert.ToInt32(dr["idVeterinario"]),
                IdUsuario = Convert.ToInt32(dr["idUsuario"]),
                Especialidad = dr["especialidad"].ToString(),
                Estado = Convert.ToBoolean(dr["estado"]),

                Nombres = dr["nombres"].ToString(),
                ApellidoPaterno = dr["apellidoPaterno"].ToString(),
                ApellidoMaterno = dr["apellidoMaterno"].ToString(),

                TipoDocumento = dr["tipoDocumento"].ToString(),
                NumeroDocumento = dr["numeroDocumento"].ToString(),
                Celular = dr["celular"].ToString(),
                Direccion = dr["direccion"].ToString()
            };
        }

        // ========================================
        // LISTAR TODOS
        // ========================================
        public List<Veterinario> ListarTodos()
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
                    IdUsuario = Convert.ToInt32(dr["idUsuario"]),
                    Especialidad = dr["especialidad"].ToString(),
                    Estado = Convert.ToBoolean(dr["estado"]),

                    Nombres = dr["nombres"].ToString(),
                    ApellidoPaterno = dr["apellidoPaterno"].ToString(),
                    ApellidoMaterno = dr["apellidoMaterno"].ToString(),

                    TipoDocumento = dr["tipoDocumento"].ToString(),
                    NumeroDocumento = dr["numeroDocumento"].ToString(),
                    Celular = dr["celular"].ToString(),
                    Direccion = dr["direccion"].ToString()
                });
            }

            return lista;
        }
    }
}
