using Btc.Api.DTOs;
using Btc.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Btc.Api.Controllers
{
    [Route("api/bitcoin")]
    public class BitcoinController : Controller
    {
        private readonly IBitcoinService _bitcoinService;
        private readonly ILogger<BitcoinController> _logger;

        public BitcoinController(
            IBitcoinService bitcoinService,
            ILogger<BitcoinController> logger
        )
        {
            _bitcoinService = bitcoinService;
            _logger = logger;
        }

        [HttpGet]
        [Route("rates/latest")]
        public async Task<IActionResult> Latest(string instrument = "BTC-EUR")
        {
            _logger.LogTrace("Recieved request for latest btc rate records.");
            var latestRates = await _bitcoinService.GetLatestRecords();
            if ( latestRates == null || latestRates.Count < 1 ) return NotFound();
            return Ok(latestRates);
        }

        [HttpPost]
        [Route("rates/latest/check")]
        public async Task<IActionResult> CheckForUpdates([FromBody]TimestampDTO dto)
        {
            _logger.LogTrace("Recieved request for check if new btc rate records are available.");
            return Ok(_bitcoinService.CheckForUpdates(dto.LastTimestamp));
        }

        [HttpGet]
        [Route("rates/snapshots")]
        public async Task<IActionResult> GetSnapshots()
        {
            _logger.LogTrace("Recieved request for snapshots.");
            var snapshots = _bitcoinService.GetSnapshots();
            if (snapshots == null || snapshots.Count < 1) return NotFound();
            return Ok(snapshots);
        }

        [HttpPost]
        [Route("rates/snapshots/save")]
        public async Task SaveRecord([FromBody]BitcoinRateRecordSnapshotDTO btcRecord)
        {
            _logger.LogTrace("Recieved request to save snapshot.");
            await _bitcoinService.SaveRecord(btcRecord);
        }

        [HttpPost]
        [Route("rates/snapshots/delete-many")]
        public async Task<IActionResult> DeleteMany([FromBody] List<int> ids)
        {
            _logger.LogTrace("Recieved request to delete one or multiple snapshots.");
            await _bitcoinService.DeleteMany(ids);
            return Ok();
        }
    }
}
