using Btc.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Btc.Api.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(
            ICurrencyService currencyService
        )
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        [Route("rates/latest")]
        public async Task<IActionResult> GetLatestRates()
        {
            var latestRates = await _currencyService.GetLatestCurrencyRates();

            if (latestRates == null) return NotFound();

            return Ok(latestRates);
        }
    }
}
