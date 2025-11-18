using Microsoft.AspNetCore.Mvc;
using ClinicaVeterinaria.Services;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string usuario, string contrasena)
        {
            var user = _auth.Login(usuario, contrasena);

            if (user == null)
            {
                ViewBag.Error = "Usuario o contraseña incorrectos";
                return View();
            }

            // Guardar en session
            HttpContext.Session.SetInt32("idUsuario", user.IdUsuario);
            HttpContext.Session.SetString("rol", user.Rol);

            return RedirectToAction("Index", "Home");
        }
    }
}