using ClinicaVeterinaria.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClinicaVeterinaria.Data
{
    public class ClienteRepository
    {
        private readonly SqlConnectionFactory _factory;

        public ClienteRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public int Insertar(Cliente c)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_InsertarCliente", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idUsuario", c.IdUsuario);
            cmd.Parameters.AddWithValue("@nombres", c.Nombres);
            cmd.Parameters.AddWithValue("@apellidoPaterno", c.ApellidoPaterno);
            cmd.Parameters.AddWithValue("@apellidoMaterno", c.ApellidoMaterno);
            cmd.Parameters.AddWithValue("@tipoDocumento", c.TipoDocumento);
            cmd.Parameters.AddWithValue("@numeroDocumento", c.NumeroDocumento);
            cmd.Parameters.AddWithValue("@celular", c.Celular ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@direccion", c.Direccion ?? (object)DBNull.Value);

            con.Open();

            var result = cmd.ExecuteScalar();
            return result == null ? 0 : Convert.ToInt32(result);
        }

        public int Actualizar(Cliente c)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ActualizarCliente", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idCliente", c.IdCliente);
            cmd.Parameters.AddWithValue("@idUsuario", c.IdUsuario);
            cmd.Parameters.AddWithValue("@nombres", c.Nombres);
            cmd.Parameters.AddWithValue("@apellidoPaterno", c.ApellidoPaterno);
            cmd.Parameters.AddWithValue("@apellidoMaterno", c.ApellidoMaterno);
            cmd.Parameters.AddWithValue("@tipoDocumento", c.TipoDocumento);
            cmd.Parameters.AddWithValue("@numeroDocumento", c.NumeroDocumento);
            cmd.Parameters.AddWithValue("@celular", c.Celular ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@direccion", c.Direccion ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@estado", c.Estado);

            con.Open();

            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }

        public int Eliminar(int idCliente)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_EliminarCliente", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idCliente", idCliente);

            con.Open();

            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }

        public Cliente ObtenerPorId(int idCliente)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ObtenerClientePorId", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idCliente", idCliente);

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (!dr.Read()) return null;

            return new Cliente
            {
                IdCliente = Convert.ToInt32(dr["idCliente"]),
                IdUsuario = Convert.ToInt32(dr["idUsuario"]),
                Nombres = dr["nombres"].ToString(),
                ApellidoPaterno = dr["apellidoPaterno"].ToString(),
                ApellidoMaterno = dr["apellidoMaterno"].ToString(),
                TipoDocumento = dr["tipoDocumento"].ToString(),
                NumeroDocumento = dr["numeroDocumento"].ToString(),
                Celular = dr["celular"]?.ToString(),
                Direccion = dr["direccion"]?.ToString(),
                Estado = Convert.ToBoolean(dr["estado"])
            };
        }

        public List<Cliente> ListarTodos()
        {
            var lista = new List<Cliente>();

            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarClientes", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new Cliente
                {
                    IdCliente = Convert.ToInt32(dr["idCliente"]),
                    IdUsuario = Convert.ToInt32(dr["idUsuario"]),
                    Nombres = dr["nombres"].ToString(),
                    ApellidoPaterno = dr["apellidoPaterno"].ToString(),
                    ApellidoMaterno = dr["apellidoMaterno"].ToString(),
                    TipoDocumento = dr["tipoDocumento"].ToString(),
                    NumeroDocumento = dr["numeroDocumento"].ToString(),
                    Celular = dr["celular"]?.ToString(),
                    Direccion = dr["direccion"]?.ToString(),
                    Estado = Convert.ToBoolean(dr["estado"])
                });
            }

            return lista;
        }
    }
}