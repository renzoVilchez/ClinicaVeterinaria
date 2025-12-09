using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    public class ServicioController : Controller
    {
        private readonly ServicioRepository _repo;

        public ServicioController(ServicioRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var lista = _repo.ListarTodos();
            return Json(lista);
        }

        [HttpGet]
        public IActionResult Obtener(int id)
        {
            var s = _repo.ObtenerPorId(id);
            if (s == null) return NotFound();
            return Json(s);
        }

        [HttpPost]
        public IActionResult Guardar([FromBody] Servicio servicio)
        {
            if (servicio == null) return BadRequest();

            if (servicio.IdServicio == 0)
            {
                var newId = _repo.Insertar(servicio);
                return Ok(new { success = true, id = newId });
            }
            else
            {
                var rows = _repo.Actualizar(servicio);
                return Ok(new { success = rows > 0 });
            }
        }

        [HttpPost]
        public IActionResult Eliminar([FromBody] int id)
        {
            var rows = _repo.Eliminar(id);
            return Ok(new { success = rows > 0 });
        }
    }
}
