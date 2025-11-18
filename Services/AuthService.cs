using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Services
{
    public class AuthService
    {
        private readonly UsuarioRepository _repo;

        public AuthService(UsuarioRepository repo)
        {
            _repo = repo;
        }

        public Usuario Login(string usuario, string contrasena)
        {
            return _repo.Login(usuario, contrasena);
        }
    }
}