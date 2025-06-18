using AIProgram.Web.Services;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;

namespace AIProgram.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [Route("getsummary")]
        public string GetSummary(string url)
        {
            var scraper = new ScraperService();;
            var ollama = new OllamaService();
            return ollama.GetSummary(scraper.Scrape(url));

        }
    }
}
