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
            var c = _repo.ObtenerPorId(id);
            if (c == null) return NotFound();
            return Json(c);
        }

        [HttpPost]
        public IActionResult Guardar([FromBody] Cliente c)
        {
            if (c == null) return BadRequest();

            if (c.IdCliente == 0)
            {
                // Inserción normal
                var newId = _repo.Insertar(c);
                return Ok(new { success = true, id = newId });
            }
            else
            {
                // 🔥 Traemos el cliente original para no perder su IdUsuario
                var original = _repo.ObtenerPorId(c.IdCliente);
                if (original == null) return NotFound();

                // 🔥 Mantener el IdUsuario original
                c.IdUsuario = original.IdUsuario;

                var rows = _repo.Actualizar(c);
                return Ok(new { success = rows > 0 });
            }
        }


        [HttpPost]
        public IActionResult Eliminar([FromBody] int id)
        {
            var rows = _repo.EliminarConUsuario(id);
            return Ok(new { success = rows > 0 });
        }

        [HttpPost]
        public IActionResult RegistrarCompleto([FromBody] ClienteCompleto model)
        {
            var idUsuario = _repo.RegistrarClienteCompleto(model);
            return Ok(new { success = idUsuario > 0, idUsuario });
        }

        public IActionResult RegistrarCompletoView()
        {
            return View("RegistrarCompleto");
        }

    }
}
