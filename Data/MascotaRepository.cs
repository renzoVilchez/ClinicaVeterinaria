using System.Data;
using ClinicaVeterinaria.Models;
using Microsoft.Data.SqlClient;

namespace ClinicaVeterinaria.Data
{
    public class MascotaRepository
    {
        private readonly SqlConnectionFactory _factory;

        public MascotaRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<Mascota> ListarTodos()
        {
            var lista = new List<Mascota>();

            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarMascotas", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new Mascota
                {
                    IdMascota = Convert.ToInt32(dr["idMascota"]),
                    IdCliente = Convert.ToInt32(dr["idCliente"]),
                    IdEspecie = Convert.ToInt32(dr["idEspecie"]),
                    Nombre = dr["nombre"].ToString(),
                    Raza = dr["raza"].ToString(),
                    Edad = dr["edad"] != DBNull.Value ? (int?)Convert.ToInt32(dr["edad"]) : null,
                    Peso = dr["peso"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["peso"]) : null,
                    Sexo = dr["sexo"].ToString(),
                    Estado = dr["estado"] != DBNull.Value && Convert.ToBoolean(dr["estado"]),
                    NombreCliente = dr["nombreCliente"].ToString(),  
                    NombreEspecie = dr["nombreEspecie"].ToString()
                });
            }

            return lista;
        }

        public Mascota? ObtenerPorId(int id)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ObtenerMascotaPorId", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idMascota", id);

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (!dr.Read()) return null;

            return new Mascota
            {
                IdMascota = Convert.ToInt32(dr["idMascota"]),
                IdCliente = Convert.ToInt32(dr["idCliente"]),
                IdEspecie = Convert.ToInt32(dr["idEspecie"]),
                Nombre = dr["nombre"].ToString(),
                Raza = dr["raza"].ToString(),
                Edad = dr["edad"] != DBNull.Value ? (int?)Convert.ToInt32(dr["edad"]) : null,
                Peso = dr["peso"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["peso"]) : null,
                Sexo = dr["sexo"].ToString(),
                Estado = dr["estado"] != DBNull.Value && Convert.ToBoolean(dr["estado"])
            };
        }

        public int Insertar(Mascota m)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_InsertarMascota", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idCliente", m.IdCliente);
            cmd.Parameters.AddWithValue("@idEspecie", m.IdEspecie);
            cmd.Parameters.AddWithValue("@nombre", m.Nombre);
            cmd.Parameters.AddWithValue("@raza", (object)m.Raza ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@edad", (object)m.Edad ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@peso", (object)m.Peso ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sexo", (object?)m.Sexo ?? DBNull.Value);

            con.Open();
            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public int Actualizar(Mascota m)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ActualizarMascota", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idMascota", m.IdMascota);
            cmd.Parameters.AddWithValue("@idCliente", m.IdCliente);
            cmd.Parameters.AddWithValue("@idEspecie", m.IdEspecie);
            cmd.Parameters.AddWithValue("@nombre", m.Nombre);
            cmd.Parameters.AddWithValue("@raza", (object)m.Raza ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@edad", (object)m.Edad ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@peso", (object)m.Peso ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sexo", m.Sexo);
            cmd.Parameters.AddWithValue("@estado", m.Estado);

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }

        public int Eliminar(int id)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_EliminarMascota", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idMascota", id);

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }
        public List<Mascota> ListarPorCliente(int idCliente)
        {
            var lista = new List<Mascota>();

            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarMascotasPorCliente", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idCliente", idCliente);

            con.Open();
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Mascota
                {
                    IdMascota = Convert.ToInt32(dr["idMascota"]),
                    Nombre = dr["nombre"].ToString()!
                });
            }

            return lista;
        }
    }
}
