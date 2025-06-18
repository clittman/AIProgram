using AngleSharp.Io;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace AIProgram.Web.Services
{
    public class OllamaService
    {
        public string GetSummary(string text)
        {
            var prompt = $"Write a concise, paragraph-long summary of the news article below. Make it clear, engaging, and easy to understand. Do not include introductory phrases such as 'here is a summary'. {text}";

            var ollamaRequest = new
            {
                model = "llama3",
                prompt,
                stream = false
            };

            var json = JsonSerializer.Serialize(ollamaRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync("https://api.lit-ai-demo.com/api/generate", content).Result;
            return response.Content.ReadFromJsonAsync<OllamaResponse>().Result.Response;
        }

        public class OllamaResponse
        {
            public string Response { get; set; }
        }
    }
}
