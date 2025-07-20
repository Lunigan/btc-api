using Btc.Api.DTOs.CoinDesk;
using Btc.Api.Models;
using Btc.Api.Repositories.Interfaces;
using Btc.Api.Services.Interfaces;

namespace Btc.Api.Services
{
    public class BitcoinService : IBitcoinService
    {
        private readonly IHttpService _httpService;
        private readonly IUnitOfWork _unitOfWork;
        private const string COIN_DESK_BTC_EUR_API_URI = "https://data-api.coindesk.com/spot/v1/latest/tick?market=coinbase&instruments=BTC-EUR";

        public BitcoinService(
            IHttpService httpService,
            IUnitOfWork unitOfWork
        )
        {
            _httpService = httpService;
            _unitOfWork = unitOfWork;
        }

        public async Task FetchCoinDeskRateAsync()
        {
            var response = await _httpService.GetAsync<CoinDeskRootResponse>(COIN_DESK_BTC_EUR_API_URI);

            if (response?.Data.TryGetValue("BTC-EUR", out var instrument) == true)
            {
                await _unitOfWork.BitcoinRates.AddAsync(new()
                {
                    BtcEur = instrument.Price,
                    Timestamp = DateTime.UtcNow
                });
                await _unitOfWork.SaveAsync();
            }
            else
            {
                // log error - no valid data
            }

        }

        public async Task<List<BitcoinRateRecord>> GetLatestRecords()
        {
            var latest = _unitOfWork.BitcoinRates.GetLatest();
        }

        public List<BitcoinRateRecord> GetRecordsFromLastThreeDays()
        {
            var latest = _unitOfWork.BitcoinRates.GetRecordsFromLastDays(3);
        }
    }
}
