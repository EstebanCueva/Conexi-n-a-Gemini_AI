using System.ComponentModel.DataAnnotations;

namespace Conexión_a_Gemini_AI.Models
{
    public class TogetherAi
    {
        public string model { get; set; }
        public List<message> messages { get; set; }

        public class message
        {
            public string role { get; set; }
            public string content { get; set; }
        }
        public int Id;
    }
}
