using Conexión_a_Gemini_AI.Interfaces;

namespace Conexión_a_Gemini_AI.Repositories
{
    public class OpenAIRepository: IChatBotServices
    {
        private HttpClient _httpClient;
        private string openAiApiKey = "sk-4v1r7xq2J8j0X3BlbkT3T3BlbkT3T3BlbkT3T3BlbkT3T3BlbkT3T3Bl";
        public OpenAIRepository()
        {
            _httpClient = new HttpClient();
        }
        public Task<string> GetChatBotResponceAsync(string prompt)
        {
            throw new NotImplementedException();
        }
        public Task<bool> SaveResponceInDB(string chatbotPrompt, string chatbotResponse)
        {
            throw new NotImplementedException();
        }
    }
}
