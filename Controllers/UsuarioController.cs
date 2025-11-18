using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository _repo;

        public UsuarioController(UsuarioRepository repo)
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
        public IActionResult Guardar([FromBody] Usuario usuario)
        {
            if (usuario == null) return BadRequest();

            if (usuario.IdUsuario == 0)
            {
                var newId = _repo.Insertar(usuario);
                return Ok(new { success = true, id = newId });
            }
            else
            {
                var rows = _repo.Actualizar(usuario);
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