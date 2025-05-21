using System.ComponentModel.DataAnnotations;

namespace Conexión_a_Gemini_AI.Models
{
    public class ChatAnswer
    {
        public int Id { get; set; }

        public string Prompt { get; set; }
        public string Response { get; set; }
        public string Provider { get; set; }
        public int TokensUsed { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
