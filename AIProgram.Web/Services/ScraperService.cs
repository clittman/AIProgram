using AngleSharp.Html.Parser;

namespace AIProgram.Web.Services
{
    public class ScraperService
    {
        public string Scrape(string url)
        {
            using var client = new HttpClient();
            var html = client.GetStringAsync(url).Result;
            return ExtractCleanText(html);
        }

        private string ExtractCleanText(string html)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);


            foreach (var node in document.QuerySelectorAll("style, script, nav, header, footer, aside"))
            {
                node.Remove();
            }

            var selectors = new[] { ".post-content", ".entry-content", ".article-content", ".main-content", "article" };

            foreach (var selector in selectors)
            {
                var element = document.QuerySelector(selector);
                if (element != null)
                {
                    var text = element.TextContent.Trim();
                    if (!string.IsNullOrWhiteSpace(text) && text.Length > 200)
                    {
                        return text;
                    }
                }
            }

            var candidates = document.Body.QuerySelectorAll("div, section, p")
                .Select(e => new { Element = e, Text = e.TextContent?.Trim() ?? "" })
                .Where(e => e.Text.Length > 100)
                .OrderByDescending(e => e.Text.Length);

            return candidates.FirstOrDefault()?.Text ?? string.Empty;
        }
    }
}
