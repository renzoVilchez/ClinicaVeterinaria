using ClinicaVeterinaria.Data;
using Microsoft.AspNetCore.Mvc;

public class ClienteController : Controller
{
    private readonly ClienteRepository _repo;
    public ClienteController(ClienteRepository repo) { _repo = repo; }

    [HttpGet]
    public IActionResult Listar()
    {
        var lista = _repo.ListarTodos(); // Debes tener un método que devuelva todos los clientes
        return Json(lista);
    }
}
