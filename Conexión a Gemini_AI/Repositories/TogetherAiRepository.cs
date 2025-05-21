using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Conexión_a_Gemini_AI.Interfaces;
using Conexión_a_Gemini_AI.Models;
using static Conexión_a_Gemini_AI.Models.TogetherAi;


namespace Conexión_a_Gemini_AI.Repositories
{
    public class TogetherAiRepository: IChatBotServices
    {
        private readonly HttpClient _httpClient;
        private string apiKey = "4347dbe12518ffe873ab8bef5750e87326788ece51878cdb43511513d190967e";

        public TogetherAiRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetChatBotResponceAsync(string prompt)
        {
            string url = "https://api.together.xyz/v1/chat/completions";

            TogetherAi request = new TogetherAi
            {
                model = "deepseek-ai/DeepSeek-V3",
                messages = new List<message>
                {
                    new message
                    {
                        role = "user",
                        content = prompt
                    }
                }
            };

            var json = JsonSerializer.Serialize(request);
            var message = new HttpRequestMessage(HttpMethod.Post, url);
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(message);
            string result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
