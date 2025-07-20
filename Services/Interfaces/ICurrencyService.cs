using Btc.Api.DTOs.Responses;
using Btc.Api.Models;

namespace Btc.Api.Services.Interfaces
{
    public interface ICurrencyService
    {
        Task<List<CurrencyRateResponse?>?> GetLatestCurrencyRates();
        CurrencyRateResponse? GetLatestCurrencyRate(string currencyCode);
        Task<bool> TryFetchCurrentRates();
    }
}
