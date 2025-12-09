using Microsoft.AspNetCore.Mvc;
using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Controllers
{
    public class ChatBotController : Controller
    {
        private readonly ChatBotRepository _repo;

        public ChatBotController(ChatBotRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var lista = _repo.ListarTodos();
            return View(lista);
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
            var registro = _repo.ObtenerPorId(id);
            return Json(registro);
        }


        [HttpPost]
        public IActionResult Guardar([FromBody] ChatBot c)
        {
            if (c.IdPregunta == 0)
                _repo.Insertar(c);
            else
                _repo.Actualizar(c);

            return Json(new { success = true });
        }


        [HttpPost]
        public IActionResult Eliminar([FromBody] int id)
        {
            _repo.Eliminar(id);
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult Cliente()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Responder(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return Json(new { respuesta = "Por favor escribe algo 😄" });

            texto = NormalizarTexto(texto);

            var lista = _repo.ListarTodos();

            ChatBot mejorCoincidencia = null;
            double mayorSimilitud = 0;

            foreach (var item in lista)
            {
                var preguntaBD = NormalizarTexto(item.Pregunta);

                // Similitud Levenshtein
                double similitud = CalcularSimilitud(texto, preguntaBD);

                if (similitud > mayorSimilitud)
                {
                    mayorSimilitud = similitud;
                    mejorCoincidencia = item;
                }

                // MATCH DIRECTO por palabra clave principal
                if (preguntaBD.Contains("vacuna") && texto.Contains("vacuna"))
                {
                    return Json(new { respuesta = item.Respuesta });
                }

                if (preguntaBD.Contains("gato") && texto.Contains("gato"))
                {
                    return Json(new { respuesta = item.Respuesta });
                }

                if (preguntaBD.Contains("consulta") && texto.Contains("consulta"))
                {
                    return Json(new { respuesta = item.Respuesta });
                }
            }

            // Ajuste del umbral para aceptar coincidencias
            if (mayorSimilitud < 0.30)
                return Json(new { respuesta = "Lo siento, no tengo información sobre eso." });

            return Json(new { respuesta = mejorCoincidencia.Respuesta });
        }


        // Normaliza texto a una forma comparable
        private string NormalizarTexto(string texto)
        {
            texto = texto.ToLower().Trim();
            texto = texto
                    .Replace("¿", "")
                    .Replace("?", "")
                    .Replace("á", "a")
                    .Replace("é", "e")
                    .Replace("í", "i")
                    .Replace("ó", "o")
                    .Replace("ú", "u");
            return texto;
        }

        // Cálculo de la similitud entre dos textos usando Levenshtein
        private double CalcularSimilitud(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2))
                return 1;

            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return 0;

            int distancia = DistanciaLevenshtein(s1, s2);
            int longitudMax = Math.Max(s1.Length, s2.Length);

            return 1.0 - ((double)distancia / longitudMax);
        }

        // Algoritmo Levenshtein
        private int DistanciaLevenshtein(string s, string t)
        {
            int[,] matriz = new int[s.Length + 1, t.Length + 1];

            for (int i = 0; i <= s.Length; i++)
                matriz[i, 0] = i;

            for (int j = 0; j <= t.Length; j++)
                matriz[0, j] = j;

            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= t.Length; j++)
                {
                    int costo = (s[i - 1] == t[j - 1]) ? 0 : 1;

                    matriz[i, j] = Math.Min(
                        Math.Min(matriz[i - 1, j] + 1, matriz[i, j - 1] + 1),
                        matriz[i - 1, j - 1] + costo
                    );
                }
            }

            return matriz[s.Length, t.Length];
        }



    }
}
