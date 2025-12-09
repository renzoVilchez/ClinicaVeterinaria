using System.Data;
using ClinicaVeterinaria.Models;
using Microsoft.Data.SqlClient;

namespace ClinicaVeterinaria.Data
{
    public class ServicioRepository
    {
        private readonly SqlConnectionFactory _factory;

        public ServicioRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<Servicio> ListarTodos()
        {
            var lista = new List<Servicio>();

            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarServicios", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Servicio
                {
                    IdServicio = Convert.ToInt32(dr["idServicio"]),
                    NombreServicio = dr["nombreServicio"].ToString(),
                    Descripcion = dr["descripcion"].ToString(),
                    Precio = Convert.ToDecimal(dr["precio"])
                });
            }

            return lista;
        }

        public Servicio? ObtenerPorId(int id)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ObtenerServicioPorId", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idServicio", id);

            con.Open();
            using var dr = cmd.ExecuteReader();

            if (!dr.Read())
                return null;

            return new Servicio
            {
                IdServicio = Convert.ToInt32(dr["idServicio"]),
                NombreServicio = dr["nombreServicio"].ToString(),
                Descripcion = dr["descripcion"].ToString(),
                Precio = Convert.ToDecimal(dr["precio"])
            };
        }

        public int Insertar(Servicio s)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_InsertarServicio", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nombreServicio", s.NombreServicio);
            cmd.Parameters.AddWithValue("@descripcion", s.Descripcion ?? "");
            cmd.Parameters.AddWithValue("@precio", s.Precio);

            con.Open();
            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public int Actualizar(Servicio s)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ActualizarServicio", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idServicio", s.IdServicio);
            cmd.Parameters.AddWithValue("@nombreServicio", s.NombreServicio);
            cmd.Parameters.AddWithValue("@descripcion", s.Descripcion ?? "");
            cmd.Parameters.AddWithValue("@precio", s.Precio);

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }

        public int Eliminar(int id)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_EliminarServicio", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idServicio", id);

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }
    }
}
