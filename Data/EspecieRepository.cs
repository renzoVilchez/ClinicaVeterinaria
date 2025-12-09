using ClinicaVeterinaria.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClinicaVeterinaria.Data
{
    public class EspecieRepository
    {
        private readonly SqlConnectionFactory _factory;

        public EspecieRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public int Insertar(Especie e)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_InsertarEspecie", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nombreEspecie", e.NombreEspecie);

            con.Open();

            var result = cmd.ExecuteScalar();
            return result == null ? 0 : Convert.ToInt32(result);
        }

        public int Actualizar(Especie u)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ActualizarEspecie", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idEspecie", u.IdEspecie);
            cmd.Parameters.AddWithValue("@nombreEspecie", u.NombreEspecie);

            con.Open();

            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }

        public int Eliminar(int idEspecie)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_EliminarEspecie", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idEspecie", idEspecie);

            con.Open();

            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }

        public Especie ObtenerPorId(int idEspecie)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ObtenerEspeciePorId", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idEspecie", idEspecie);

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (!dr.Read()) return null;

            return new Especie
            {
                IdEspecie = Convert.ToInt32(dr["idEspecie"]),
                NombreEspecie = dr["nombreEspecie"].ToString(),
            };
        }

        public List<Especie> ListarTodos()
        {
            var lista = new List<Especie>();
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarEspecies", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new Especie
                {
                    IdEspecie = Convert.ToInt32(dr["idEspecie"]),
                    NombreEspecie = dr["nombreEspecie"].ToString(),
                });
            }
            return lista;
        }
    }
}
