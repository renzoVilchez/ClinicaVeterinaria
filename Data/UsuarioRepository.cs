using System.Data;
using ClinicaVeterinaria.Models;
using System.Data.SqlClient;

namespace ClinicaVeterinaria.Data
{
    public class UsuarioRepository
    {
        private readonly SqlConnectionFactory _factory;

        public UsuarioRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public Usuario Login(string usuario, string contrasena)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_Login", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@contrasena", contrasena);

            con.Open();
            using var dr = cmd.ExecuteReader();

            if (!dr.Read()) return null;

            return new Usuario
            {
                IdUsuario = Convert.ToInt32(dr["idUsuario"]),
                NombreUsuario = dr["nombreUsuario"].ToString(),
                Rol = dr["rol"].ToString()
            };
        }

        public int Insertar(Usuario u)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_InsertarUsuario", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nombreUsuario", u.NombreUsuario);
            cmd.Parameters.AddWithValue("@contrasena", u.Contrasena);
            cmd.Parameters.AddWithValue("@rol", u.Rol);

            con.Open();

            var result = cmd.ExecuteScalar();
            return result == null ? 0 : Convert.ToInt32(result);
        }

        public int Actualizar(Usuario u)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ActualizarUsuario", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idUsuario", u.IdUsuario);
            cmd.Parameters.AddWithValue("@nombreUsuario", u.NombreUsuario);
            cmd.Parameters.AddWithValue("@contrasena", u.Contrasena);
            cmd.Parameters.AddWithValue("@rol", u.Rol);
            cmd.Parameters.AddWithValue("@estado", u.Estado);

            con.Open();

            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }

        public int Eliminar(int idUsuario)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_EliminarUsuario", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

            con.Open();

            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);
            return 0;
        }

        public Usuario ObtenerPorId(int idUsuario)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ObtenerUsuarioPorId", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (!dr.Read()) return null;

            return new Usuario
            {
                IdUsuario = Convert.ToInt32(dr["idUsuario"]),
                NombreUsuario = dr["nombreUsuario"].ToString(),
                Contrasena = dr["contrasena"].ToString(),
                Rol = dr["rol"].ToString(),
                Estado = Convert.ToBoolean(dr["estado"])
            };
        }

        public List<Usuario> ListarTodos()
        {
            var lista = new List<Usuario>();
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarUsuarios", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new Usuario
                {
                    IdUsuario = Convert.ToInt32(dr["idUsuario"]),
                    NombreUsuario = dr["nombreUsuario"].ToString(),
                    Contrasena = dr["contrasena"].ToString(),
                    Rol = dr["rol"].ToString(),
                    Estado = Convert.ToBoolean(dr["estado"])
                });
            }
            return lista;
        }
    }
}