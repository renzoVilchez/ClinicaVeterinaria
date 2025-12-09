using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteRepository _repo;

        public ClienteController(ClienteRepository repo)
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
        public IActionResult Guardar([FromBody] Cliente Cliente)
        {
            if (Cliente == null) return BadRequest();

            if (Cliente.IdCliente == 0)
            {
                var newId = _repo.Insertar(Cliente);
                return Ok(new { success = true, id = newId });
            }
            else
            {
                var rows = _repo.Actualizar(Cliente);
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
    }
}