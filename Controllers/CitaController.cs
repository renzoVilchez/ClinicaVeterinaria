using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    public class CitaController : Controller
    {
        private readonly CitaRepository _repo;

        public CitaController(CitaRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Listar todas las citas
        [HttpGet]
        public IActionResult Listar()
        {
            var lista = _repo.ListarTodos();
            return Json(lista);
        }

        // GET: Obtener cita por ID
        [HttpGet]
        public IActionResult Obtener(int id)
        {
            var c = _repo.ObtenerPorId(id);
            if (c == null) return NotFound();
            return Json(c);
        }

        // POST: Insertar o actualizar
        [HttpPost]
        public IActionResult Guardar([FromBody] Cita cita)
        {
            if (cita == null) return BadRequest();

            if (cita.IdCita == 0)
            {
                var newId = _repo.Insertar(cita);
                return Ok(new { success = true, id = newId });
            }
            else
            {
                var rows = _repo.Actualizar(cita);
                return Ok(new { success = rows > 0 });
            }
        }

        // POST: Eliminar
        [HttpPost]
        public IActionResult Eliminar([FromBody] int id)
        {
            var rows = _repo.Eliminar(id);
            return Ok(new { success = rows > 0 });
        }

        [HttpGet]
        public IActionResult HorariosDisponibles(int idVeterinario, DateTime fecha)
        {
            var horarios = _repo.HorariosDisponibles(idVeterinario, fecha);
            return Json(horarios); // Devuelve lista de horas disponibles ["08:00", "09:00", ...]
        }


    }
}
