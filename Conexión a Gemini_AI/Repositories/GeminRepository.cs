using Newtonsoft.Json;
using System.Text;
using Conexión_a_Gemini_AI.Interfaces;
using Conexión_a_Gemini_AI.Models;
using Newtonsoft.Json.Linq;
using static Conexión_a_Gemini_AI.Models.GeminiRequest;


namespace Conexión_a_Gemini_AI.Repositories
{
    public class GeminRepository:IChatBotServices
    {
        HttpClient _httpClient;
        private string apiKey = "AIzaSyByx-30z3Zp09aHe33exz7PmMGcKhoUoew";
        public GeminRepository()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> GetChatBotResponceAsync(string prompt)
        {
            string url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + apiKey;
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, url);

            GeminiRequest request = new GeminiRequest
            {
                contents = new List<Content>
                {
                    new Content
                    {
                        parts = new List<Part>
                        {
                            new Part
                            {
                                text = prompt
                            }
                        }
                    }
                }
            };
            message.Content = JsonContent.Create<GeminiRequest>(request);

            var response = await _httpClient.SendAsync(message);
            string answer = await response.Content.ReadAsStringAsync();

            return answer;
        }
    }
}

