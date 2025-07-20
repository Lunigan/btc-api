using Btc.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Btc.Api.Controllers
{
    public class BitcoinController : Controller
    {
        private readonly IBitcoinService _bitcoinService;

        public BitcoinController(
            IBitcoinService bitcoinService
        )
        {
            _bitcoinService = bitcoinService;
        }

        [HttpGet]
        [Route("rates/latest")]
        public IActionResult Index(string instrument = "BTC-EUR")
        {
            var latestRates = _bitcoinService.
        }
    }
}
