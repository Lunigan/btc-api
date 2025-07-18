using Btc.Api.Models;
using Btc.Api.Services.Interfaces;

namespace Btc.Api.Services
{
    public class CnbService : ICnbService
    {
        private readonly IHttpService _httpService;

        public CnbService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        // ČNB URI: https://api.cnb.cz/cnbapi/exrates/daily?date=2025-07-17&lang=EN

        public async Task<CnbCurrencyRate> FetchCnbRateAsync(DateTime date)
        {
            /*
             * Create custom table for fetch times from ČNB?
             * Only fetch once at 14:45?
             * 
             */

            return null;
        }
    }
}
