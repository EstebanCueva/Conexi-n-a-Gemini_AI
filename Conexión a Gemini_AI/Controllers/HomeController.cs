using System.Diagnostics;
using System.Text.Json;
using Conexión_a_Gemini_AI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Conexión_a_Gemini_AI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GeminRepository _gemini;
        private readonly TogetherAiRepository _togetherAi;

        public HomeController(
            ILogger<HomeController> logger,
            GeminRepository gemini,
            TogetherAiRepository togetherAi)
        {
            _logger = logger;
            _gemini = gemini;
            _togetherAi = togetherAi;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string prompt, string chatbot)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                ViewBag.chatbotAnswer = "Por favor, ingresa una pregunta.";
                ViewBag.ChatbotSeleccionado = chatbot;
                return View();
            }

            string rawResponse = "";
            string respuestaSimple = "";
            int tokens = 0;

            try
            {
                if (chatbot == "TogetherAI")
                {
                    rawResponse = await _togetherAi.GetChatBotResponceAsync(prompt);

                    using JsonDocument doc = JsonDocument.Parse(rawResponse);
                    var root = doc.RootElement;

                    if (root.TryGetProperty("choices", out JsonElement choices) &&
                        choices.GetArrayLength() > 0)
                    {
                        respuestaSimple = choices[0]
                            .GetProperty("message")
                            .GetProperty("content")
                            .GetString() ?? "No se obtuvo respuesta";

                        if (root.TryGetProperty("usage", out JsonElement usage) &&
                            usage.TryGetProperty("total_tokens", out JsonElement totalTokens))
                        {
                            tokens = totalTokens.GetInt32();
                        }
                    }
                    else
                    {
                        respuestaSimple = "Estructura de respuesta desconocida (TogetherAI).";
                    }
                }
                else if (chatbot == "Gemini")
                {
                    rawResponse = await _gemini.GetChatBotResponceAsync(prompt);

                    using JsonDocument doc = JsonDocument.Parse(rawResponse);
                    var root = doc.RootElement;

                    if (root.TryGetProperty("candidates", out JsonElement candidates) &&
                        candidates.GetArrayLength() > 0)
                    {
                        var candidate = candidates[0];

                        if (candidate.TryGetProperty("content", out JsonElement content) &&
                            content.TryGetProperty("parts", out JsonElement parts) &&
                            parts.GetArrayLength() > 0 &&
                            parts[0].TryGetProperty("text", out JsonElement texto))
                        {
                            respuestaSimple = texto.GetString() ?? "No se obtuvo respuesta";
                        }
                        else
                        {
                            respuestaSimple = "No se encontró el texto en el contenido (Gemini).";
                        }
                    }
                    else
                    {
                        respuestaSimple = "Estructura de respuesta desconocida (Gemini).";
                    }
                }
                else
                {
                    respuestaSimple = "Chatbot no reconocido.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error procesando la respuesta del chatbot");
                _logger.LogError("Respuesta cruda (debug): " + rawResponse);
                respuestaSimple = $"Error al procesar la respuesta: {ex.Message}";
            }

            ViewBag.chatbotAnswer = respuestaSimple;
            ViewBag.rawResponse = rawResponse;
            ViewBag.tokenUsage = tokens;
            ViewBag.ChatbotSeleccionado = chatbot;

            return View();
        }
    }
}
