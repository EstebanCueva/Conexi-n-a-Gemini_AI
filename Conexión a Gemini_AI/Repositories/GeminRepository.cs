using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Conexión_a_Gemini_AI.Interfaces;
using Conexión_a_Gemini_AI.Models;
using Newtonsoft.Json.Linq;

namespace Conexión_a_Gemini_AI.Repositories
{
    public class GeminRepository : IChatBotServices
    {
        private HttpClient _httpClient;
        private string geminiApiKey = "AIzaSyBZFA1dpLS1DejGK4otzvJCCmf1dgSn6SI";
        public GeminRepository()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> GetChatBotResponceAsync(string prompt)
        {
            string url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + geminiApiKey;
            GeminiRequest request = new GeminiRequest
            {
                contents = new List<GeminiContent>
                {
                    new GeminiContent
                    {
                        parts= new List<GeminiPart>
                        {
                            new GeminiPart
                            {
                                text= prompt
                            }
                        }
                    }
                }
            };
            string requestJson = JsonConvert.SerializeObject(request);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            var answer = await response.Content.ReadAsStringAsync();
            
            var jsonObj = JsonConvert.DeserializeObject<JObject>(answer);
            string respuestaTexto = (string)jsonObj["candidates"]?[0]?["content"]?["parts"]?[0]?["text"];

            return respuestaTexto ?? "No se recibió respuesta del modelo";

        }

        public Task<bool> SaveResponceInDB(string chatbotPrompt, string chatbotResponse)
        {
            throw new NotImplementedException();
        }
    }
}

