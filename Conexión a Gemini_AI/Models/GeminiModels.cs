using System.ComponentModel.DataAnnotations;

namespace Conexión_a_Gemini_AI.Models
{
    public class GeminiRequest
    {
        public List<Content> contents { get; set; }

        public class Part
        {
            public string text { get; set; }
        }

        public class Content
        {
            public List<Part> parts { get; set; }
        }

        public int Id;
    }
}
