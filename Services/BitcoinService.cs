using Btc.Api.Models;
using Btc.Api.Services.Interfaces;

namespace Btc.Api.Services
{
    public class BitcoinService : IBitcoinService
    {
        private readonly IHttpService _httpService;

        public BitcoinService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        // CoinDesk URI: https://data-api.coindesk.com/spot/v1/latest/tick?market=coinbase&instruments=BTC-EUR

        public async Task<BitcoinRateRecord> FetchCoinDeskRateAsync()
        {
            return null;
        }
    }
}
