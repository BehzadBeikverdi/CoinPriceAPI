using CoinPriceApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoinPriceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoinPricesController : ControllerBase
    {
        private readonly CoinPriceService _coinPriceService;

        public CoinPricesController(CoinPriceService coinPriceService)
        {
            _coinPriceService = coinPriceService;
        }

        [HttpGet("get-coin-prices")]
        public async Task<IActionResult> GetCoinPrices()
        {
            var prices = await _coinPriceService.GetCoinPricesAsync();

            if (prices.Count == 0)
            {
                return StatusCode(500, new { message = "Failed to fetch coin prices." });
            }

            return Ok(prices);
        }
    }
}
