using AngleSharp;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoinPriceApi.Services
{
    public class CoinPriceService
    {
        private const string Url = "https://www.tgju.org/coin";
        private readonly Dictionary<string, string> targetLabels = new()
        {
            { "سکه بهار آزادی", "Tamam" },
            { "نیم سکه", "Nim" },
            { "ربع سکه", "Rob" }
        };

        public async Task<Dictionary<string, string>> GetCoinPricesAsync()
        {
            var results = new Dictionary<string, string>();

            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync(Url);

                var context = BrowsingContext.New(Configuration.Default);
                var document = await context.OpenAsync(req => req.Content(response));

                var rows = document.QuerySelectorAll("tr");
                foreach (var row in rows)
                {
                    var label = row.QuerySelector("th")?.TextContent.Trim();
                    var value = row.QuerySelector("td.nf")?.TextContent.Trim();

                    if (label != null && value != null && targetLabels.ContainsKey(label))
                    {
                        results[targetLabels[label]] = value!;
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error fetching data: {e.Message}");
            }

            return results;
        }
    }
}
