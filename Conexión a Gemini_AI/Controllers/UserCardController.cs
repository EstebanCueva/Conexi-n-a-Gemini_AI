using Conexión_a_Gemini_AI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Conexión_a_Gemini_AI.Controllers
{
    public class UserCardController : Controller
    {
        public IActionResult Index()
        {
            var cartas = new List<UserCard>
            {
                new UserCard
                {
                    Name = "Juan Pérez",
                    PhotoUrl = "/images/juan.jpg",
                    Carrera = "Ingeniería en Sistemas",
                    Hobbies = new List<string> { "Leer", "Programar", "Tocar guitarra" }
                },
                new UserCard
                {
                    Name = "María López",
                    PhotoUrl = "/images/maria.jpg",
                    Carrera = "Diseño Gráfico",
                    Hobbies = new List<string> { "Pintar", "Fotografía", "Viajar" }
                },
                new UserCard
                {
                    Name = "Carlos García",
                    PhotoUrl = "/images/carlos.jpg",
                    Carrera = "Administración de Empresas",
                    Hobbies = new List<string> { "Correr", "Cocinar", "Jugar videojuegos" }
                }
            };

            return View(cartas);
        }
    }
}
