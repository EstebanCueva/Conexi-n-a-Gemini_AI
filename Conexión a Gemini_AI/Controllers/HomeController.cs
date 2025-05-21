using System.Diagnostics;
using Conexión_a_Gemini_AI.Models;
using Conexión_a_Gemini_AI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Conexión_a_Gemini_AI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string userPrompt)
        {
            if (string.IsNullOrWhiteSpace(userPrompt))
            {
                ViewBag.chatbotAnswer = "Por favor, ingresa una pregunta.";
                return View();
            }

            GeminRepository geminRepository = new GeminRepository();
            string responce = await geminRepository.GetChatBotResponceAsync(userPrompt);
            ViewBag.chatbotAnswer = responce;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
