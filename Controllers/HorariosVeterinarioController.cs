using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    public class HorariosVeterinarioController : Controller
    {
        private readonly HorariosVeterinarioRepository _repo;

        public HorariosVeterinarioController(HorariosVeterinarioRepository repo)
        {
            _repo = repo;
        }

        // Página principal (muestra la tabla y modal)
        public IActionResult Index()
        {
            return View();
        }

        // Listar (JSON)
        [HttpGet]
        public IActionResult Listar()
        {
            var lista = _repo.ListarTodos();
            return Json(lista);
        }

        // Obtener por id (JSON)
        [HttpGet]
        public IActionResult Obtener(int id)
        {
            var u = _repo.ObtenerPorId(id);
            if (u == null) return NotFound();
            return Json(u);
        }

        // Guardar (Insertar o Actualizar) - recibe JSON
        [HttpPost]
        public IActionResult Guardar([FromBody] HorariosVeterinario HorariosVeterinario)
        {
            if (HorariosVeterinario == null) return BadRequest();

            if (HorariosVeterinario.IdHorario == 0)
            {
                var newId = _repo.Insertar(HorariosVeterinario);
                return Ok(new { success = true, id = newId });
            }
            else
            {
                var rows = _repo.Actualizar(HorariosVeterinario);
                return Ok(new { success = rows > 0, rowsAffected = rows });
            }
        }

        // Eliminar
        [HttpPost]
        public IActionResult Eliminar([FromBody] int id)
        {
            var rows = _repo.Eliminar(id);
            return Ok(new { success = rows > 0, rowsAffected = rows });
        }

        [HttpGet]
        public IActionResult ListarVeterinarios()
        {
            var lista = _repo.ListarVeterinarios();
            return Json(lista);
        }

    }
}
