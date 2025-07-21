using Btc.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Btc.Api.Controllers
{
    [Route("api/currency")]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(
            ICurrencyService currencyService,
            ILogger<CurrencyController> logger
        )
        {
            _currencyService = currencyService;
            _logger = logger;
        }

        [HttpGet]
        [Route("rates/fetch-init")]
        public async Task<IActionResult> FetchInitialData()
        {
            _logger.LogTrace("Recieved request to fetch first currency rate records.");
            var latestRates = await _currencyService.GetLatestCurrencyRates();
            if (latestRates != null && latestRates.Count > 0) return Conflict("Currency rates are already initialized!");

            await _currencyService.TryFetchCurrentRates();
            latestRates = await _currencyService.GetLatestCurrencyRates();
            if (latestRates != null && latestRates.Count > 0) return Ok(latestRates);
            
            return NotFound();
        }

        [HttpGet]
        [Route("rates/latest")]
        public async Task<IActionResult> GetLatestRates()
        {
            _logger.LogTrace("Recieved request for latest currency rate records.");
            var latestRates = await _currencyService.GetLatestCurrencyRates();

            if (latestRates == null || latestRates.Count < 1) return NotFound();

            return Ok(latestRates);
        }
    }
}
