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
            using var cmd = new SqlCommand("sp_ListarCitas", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Cita
                {
                    IdCita = Convert.ToInt32(dr["idCita"]),
                    IdMascota = Convert.ToInt32(dr["idMascota"]),
                    MascotaNombre = dr["MascotaNombre"].ToString(),
                    IdServicio = Convert.ToInt32(dr["idServicio"]),
                    ServicioNombre = dr["ServicioNombre"].ToString(),
                    IdVeterinario = Convert.ToInt32(dr["idVeterinario"]),
                    VeterinarioNombre = dr["VeterinarioNombre"].ToString(),
                    FechaCita = Convert.ToDateTime(dr["fechaCita"]),
                    HoraCita = TimeSpan.Parse(dr["horaCita"].ToString()),
                    Estado = dr["estado"].ToString(),
                    FechaRegistro = Convert.ToDateTime(dr["fechaRegistro"])
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
            using var cmd = new SqlCommand("sp_ObtenerCitaPorId", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idCita", id);

            con.Open();
            using var dr = cmd.ExecuteReader();

            if (!dr.Read()) return null;

            return new Cita
            {
                IdCita = Convert.ToInt32(dr["idCita"]),
                IdMascota = Convert.ToInt32(dr["idMascota"]),
                IdServicio = Convert.ToInt32(dr["idServicio"]),
                IdVeterinario = Convert.ToInt32(dr["idVeterinario"]),
                FechaCita = Convert.ToDateTime(dr["fechaCita"]),
                HoraCita = TimeSpan.Parse(dr["horaCita"].ToString()),
                Estado = dr["estado"].ToString(),
                FechaRegistro = Convert.ToDateTime(dr["fechaRegistro"])
            };
        }

        // ========================
        // INSERTAR
        // ========================
        public int Insertar(Cita c)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_InsertarCita", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idMascota", c.IdMascota);
            cmd.Parameters.AddWithValue("@idServicio", c.IdServicio);
            cmd.Parameters.AddWithValue("@idVeterinario", c.IdVeterinario);
            cmd.Parameters.AddWithValue("@fechaCita", c.FechaCita);
            cmd.Parameters.AddWithValue("@horaCita", c.HoraCita);
            cmd.Parameters.AddWithValue("@estado", c.Estado);

            con.Open();
            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        // ========================
        // ACTUALIZAR
        // ========================
        public int Actualizar(Cita c)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_ActualizarCita", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idCita", c.IdCita);
            cmd.Parameters.AddWithValue("@idMascota", c.IdMascota);
            cmd.Parameters.AddWithValue("@idServicio", c.IdServicio);
            cmd.Parameters.AddWithValue("@idVeterinario", c.IdVeterinario);
            cmd.Parameters.AddWithValue("@fechaCita", c.FechaCita);
            cmd.Parameters.AddWithValue("@horaCita", c.HoraCita);
            cmd.Parameters.AddWithValue("@estado", c.Estado);

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);

            return 0;
        }

        // ========================
        // ELIMINAR
        // ========================
        public int Eliminar(int id)
        {
            using var con = _factory.CreateConnection();
            using var cmd = new SqlCommand("sp_EliminarCita", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idCita", id);

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (dr.Read()) return Convert.ToInt32(dr["RowsAffected"]);

            return 0;
        }

        public List<string> HorariosDisponibles(int idVeterinario, DateTime fecha)
        {
            var lista = new List<string>();
            using var con = _factory.CreateConnection();

            // Obtenemos horario del veterinario
            using var cmd = new SqlCommand(@"
        SELECT horaInicio, horaFin
        FROM HorariosVeterinarios
        WHERE idVeterinario = @idVet AND diaSemana = @dia
    ", con);

            cmd.Parameters.AddWithValue("@idVet", idVeterinario);
            cmd.Parameters.AddWithValue("@dia", fecha.DayOfWeek.ToString());

            con.Open();
            using var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                var inicio = (TimeSpan)dr["horaInicio"];
                var fin = (TimeSpan)dr["horaFin"];

                // Obtenemos horas ya reservadas
                var ocupadasCmd = new SqlCommand(@"
            SELECT horaCita
            FROM Citas
            WHERE idVeterinario = @idVet AND fechaCita = @fecha
        ", con);
                ocupadasCmd.Parameters.AddWithValue("@idVet", idVeterinario);
                ocupadasCmd.Parameters.AddWithValue("@fecha", fecha);

                var ocupadas = new List<TimeSpan>();
                using var dr2 = ocupadasCmd.ExecuteReader();
                while (dr2.Read())
                    ocupadas.Add((TimeSpan)dr2["horaCita"]);

                // Generamos horarios disponibles cada hora
                for (var h = inicio.Hours; h < fin.Hours; h++)
                {
                    var hora = new TimeSpan(h, 0, 0);
                    if (!ocupadas.Contains(hora))
                        lista.Add(hora.ToString(@"hh\:mm"));
                }
            }

            return lista;
        }



    }
}
