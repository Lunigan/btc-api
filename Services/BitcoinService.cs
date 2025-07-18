using Btc.Api.DTOs.CoinDesk;
using Btc.Api.Models;
using Btc.Api.Services.Interfaces;

namespace Btc.Api.Services
{
    public class BitcoinService : IBitcoinService
    {
        private readonly IHttpService _httpService;
        private const string COIN_DESK_BTC_EUR_API_URI = "https://data-api.coindesk.com/spot/v1/latest/tick?market=coinbase&instruments=BTC-EUR";

        public BitcoinService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<BitcoinRateRecord?> FetchCoinDeskRateAsync()
        {
            var response = await _httpService.GetAsync<CoinDeskRootResponse>(COIN_DESK_BTC_EUR_API_URI);

            if (response?.Data.TryGetValue("BTC-EUR", out var instrument) == true)
                return new()
                {
                    BtcEur = instrument.Price,
                    Timestamp = DateTime.UtcNow
                };

            return null; // log error
        }
    }
}
