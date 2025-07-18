using Btc.Api.DTOs.Cnb;
using Btc.Api.Models;
using Btc.Api.Repositories.Interfaces;
using Btc.Api.Services.Interfaces;

namespace Btc.Api.Services
{
    public class CnbService : ICnbService
    {
        private readonly IHttpService _httpService;
        private readonly IUnitOfWork _unitOfWork;

        private const string CNB_CURRENCY_RAES_API_URI = "https://api.cnb.cz/cnbapi/exrates/daily";

        public CnbService(
            IHttpService httpService,
            IUnitOfWork unitOfWork
        )
        {
            _httpService = httpService;
            _unitOfWork = unitOfWork;
        }

        // ČNB URI: https://api.cnb.cz/cnbapi/exrates/daily?date=2025-07-17&lang=EN

        public async Task<CnbCurrencyRate?> FetchCnbRateAsync(DateTime date)
        {
            var today = TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time")
            );

            var rateExists = _unitOfWork.CnbRates.GetByDate(today.Date);
            if (rateExists != null) return rateExists;

            // do této doby není k dispozici kurz na aktuální datum -> pracovat s tím latest
            if (rateExists == null && today.Hour >= 14 && today.Minute >= 45)
            {
                var query = new Dictionary<string, string> {
                    { "date", $"{today.Date:yyyy-MM-dd}" },
                    { "lang", "EN" }
                };

                var response = await _httpService.GetAsync<CnbRootResponse>(CNB_CURRENCY_RAES_API_URI, query);

                if (response != null)
                {
                    CnbCurrencyRate eurCurrencyRate = null;

                    foreach (var item in response.Rates)
                    {
                        var currencyRate = new CnbCurrencyRate()
                        {
                            Amount = item.Amount,
                            CurrencyCode = item.CurrencyCode,
                            Rate = item.Rate,
                            Timestamp = DateTime.UtcNow,
                            ValidFor = item.ValidFor
                        };

                        if (item.CurrencyCode.Equals("EUR")) eurCurrencyRate = currencyRate;

                        await _unitOfWork.CnbRates.AddAsync(currencyRate);
                    }
                    // log warning if null
                    return eurCurrencyRate;  
                }
            }

            return null;
        }
    }
}
