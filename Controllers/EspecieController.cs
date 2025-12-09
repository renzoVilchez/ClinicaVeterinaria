using ClinicaVeterinaria.Data;
using Microsoft.AspNetCore.Mvc;

public class EspecieController : Controller
{
    private readonly EspecieRepository _repo;
    public EspecieController(EspecieRepository repo) { _repo = repo; }

    [HttpGet]
    public IActionResult Listar()
    {
        var lista = _repo.ListarTodos(); // Devuelve todas las especies
        return Json(lista);
    }
}