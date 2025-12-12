using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    public class MascotaController : Controller
    {
        private readonly MascotaRepository _repo;

        public MascotaController(MascotaRepository repo)
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
            var m = _repo.ObtenerPorId(id);
            if (m == null) return NotFound();
            return Json(m);
        }

        [HttpGet]
        public JsonResult ListarPorCliente(int idCliente)
        {
            var data = _repo.ListarPorCliente(idCliente);
            return Json(data);
        }

        [HttpPost]
        public IActionResult Guardar([FromBody] Mascota mascota)
        {
            if (mascota == null) return BadRequest();

            if (mascota.IdMascota == 0)
            {
                var newId = _repo.Insertar(mascota);
                return Ok(new { success = true, id = newId });
            }
            else
            {
                var rows = _repo.Actualizar(mascota);
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
