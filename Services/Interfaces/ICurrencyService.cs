using Btc.Api.DTOs;

namespace Btc.Api.Services.Interfaces
{
    public interface ICurrencyService
    {
        Task<List<CurrencyRateDTO>> GetLatestCurrencyRates();
        CurrencyRateDTO? GetLatestCurrencyRate(string currencyCode);
        Task<bool> TryFetchCurrentRates();
    }
}
