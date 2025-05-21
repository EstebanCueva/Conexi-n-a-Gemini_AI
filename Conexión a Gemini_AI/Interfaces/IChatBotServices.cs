namespace Conexión_a_Gemini_AI.Interfaces
{
    public interface IChatBotServices
    {
        public Task<string> GetChatBotResponceAsync(string prompt);
    }
}
