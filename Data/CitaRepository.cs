using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Data
{
    public class CitaRepository
    {
        private readonly SqlConnectionFactory _factory;

        public CitaRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        // ========================
        // LISTAR TODAS LAS CITAS
        // ========================
        public List<Cita> ListarTodos()
        {
            var lista = new List<Cita>();

            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ListarCitas", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            con.Open();
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Cita
                {
                    IdCita = Convert.ToInt32(dr["idCita"]),
                    // idCliente (viene del SP)
                    IdCliente = dr["idCliente"] != DBNull.Value ? Convert.ToInt32(dr["idCliente"]) : 0,
                    ClienteNombre = dr["clienteNombre"] != DBNull.Value ? dr["clienteNombre"].ToString()! : string.Empty,

                    IdMascota = dr["idMascota"] != DBNull.Value ? Convert.ToInt32(dr["idMascota"]) : 0,
                    MascotaNombre = dr["mascotaNombre"] != DBNull.Value ? dr["mascotaNombre"].ToString()! : string.Empty,

                    IdServicio = dr["idServicio"] != DBNull.Value ? Convert.ToInt32(dr["idServicio"]) : 0,
                    ServicioNombre = dr["servicioNombre"] != DBNull.Value ? dr["servicioNombre"].ToString()! : string.Empty,

                    IdVeterinario = dr["idVeterinario"] != DBNull.Value ? Convert.ToInt32(dr["idVeterinario"]) : 0,
                    VeterinarioNombre = dr["veterinarioNombre"] != DBNull.Value ? dr["veterinarioNombre"].ToString()! : string.Empty,

                    FechaCita = dr["fechaCita"] != DBNull.Value ? Convert.ToDateTime(dr["fechaCita"]) : DateTime.MinValue,
                    HoraCita = dr["horaCita"] != DBNull.Value ? TimeSpan.Parse(dr["horaCita"].ToString()!) : TimeSpan.Zero,
                    Estado = dr["estado"] != DBNull.Value ? dr["estado"].ToString()! : string.Empty,
                    FechaRegistro = dr["fechaRegistro"] != DBNull.Value ? Convert.ToDateTime(dr["fechaRegistro"]) : DateTime.MinValue
                });
            }

            return lista;
        }


        // ========================
        // OBTENER POR ID
        // ========================
        public Cita? ObtenerPorId(int id)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ObtenerCitaPorId", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@idCita", id);

            con.Open();
            using var dr = cmd.ExecuteReader();

            if (!dr.Read()) return null;

            return new Cita
            {
                IdCita = Convert.ToInt32(dr["idCita"]),
                IdCliente = dr["idCliente"] != DBNull.Value ? Convert.ToInt32(dr["idCliente"]) : 0,
                ClienteNombre = dr["clienteNombre"] != DBNull.Value ? dr["clienteNombre"].ToString()! : string.Empty,

                IdMascota = dr["idMascota"] != DBNull.Value ? Convert.ToInt32(dr["idMascota"]) : 0,
                MascotaNombre = dr["mascotaNombre"] != DBNull.Value ? dr["mascotaNombre"].ToString()! : string.Empty,

                IdServicio = dr["idServicio"] != DBNull.Value ? Convert.ToInt32(dr["idServicio"]) : 0,
                ServicioNombre = dr["servicioNombre"] != DBNull.Value ? dr["servicioNombre"].ToString()! : string.Empty,

                IdVeterinario = dr["idVeterinario"] != DBNull.Value ? Convert.ToInt32(dr["idVeterinario"]) : 0,
                VeterinarioNombre = dr["veterinarioNombre"] != DBNull.Value ? dr["veterinarioNombre"].ToString()! : string.Empty,

                FechaCita = dr["fechaCita"] != DBNull.Value ? Convert.ToDateTime(dr["fechaCita"]) : DateTime.MinValue,
                HoraCita = dr["horaCita"] != DBNull.Value ? TimeSpan.Parse(dr["horaCita"].ToString()!) : TimeSpan.Zero,
                Estado = dr["estado"] != DBNull.Value ? dr["estado"].ToString()! : string.Empty,
                FechaRegistro = dr["fechaRegistro"] != DBNull.Value ? Convert.ToDateTime(dr["fechaRegistro"]) : DateTime.MinValue
            };
        }

        // ========================
        // INSERTAR
        // ========================
        public int Insertar(Cita c)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_InsertarCita", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@idMascota", c.IdMascota);
            cmd.Parameters.AddWithValue("@idServicio", c.IdServicio);
            cmd.Parameters.AddWithValue("@idVeterinario", c.IdVeterinario);
            cmd.Parameters.AddWithValue("@fechaCita", c.FechaCita);
            cmd.Parameters.AddWithValue("@horaCita", c.HoraCita);
            cmd.Parameters.AddWithValue("@estado", c.Estado);

            con.Open();
            var result = cmd.ExecuteScalar();
            return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }

        // ========================
        // ACTUALIZAR
        // ========================
        public int Actualizar(Cita c)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ActualizarCita", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@idCita", c.IdCita);
            cmd.Parameters.AddWithValue("@idMascota", c.IdMascota);
            cmd.Parameters.AddWithValue("@idServicio", c.IdServicio);
            cmd.Parameters.AddWithValue("@idVeterinario", c.IdVeterinario);
            cmd.Parameters.AddWithValue("@fechaCita", c.FechaCita);
            cmd.Parameters.AddWithValue("@horaCita", c.HoraCita);
            cmd.Parameters.AddWithValue("@estado", c.Estado);

            con.Open();
            var scalar = cmd.ExecuteScalar();
            return scalar != null && scalar != DBNull.Value ? Convert.ToInt32(scalar) : 0;
        }

        // ========================
        // ELIMINAR
        // ========================
        public int Eliminar(int id)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_EliminarCita", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@idCita", id);

            con.Open();
            var scalar = cmd.ExecuteScalar();
            return scalar != null && scalar != DBNull.Value ? Convert.ToInt32(scalar) : 0;
        }

        // ========================
        // HORARIOS DISPONIBLES
        // ========================
        public List<string> HorariosDisponibles(int idVeterinario, DateTime fecha)
        {
            var lista = new List<string>();

            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_HorariosDisponibles", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idVeterinario", idVeterinario);
            cmd.Parameters.AddWithValue("@fecha", fecha);

            con.Open();
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(dr["hora"].ToString());
            }

            return lista;
        }
    }
}