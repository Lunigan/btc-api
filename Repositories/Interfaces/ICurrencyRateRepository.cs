using Btc.Api.Models;

namespace Btc.Api.Repositories.Interfaces
{
    public interface ICurrencyRateRepository
    {
        Task AddAsync(CurrencyRate currencyRate);
        List<CurrencyRate> GetAll();
        List<CurrencyRate> GetAll(string currencyCode);
        CurrencyRate? Get(int id);
        CurrencyRate? GetByDate(DateTime date, string currencyCode);
        Task<List<CurrencyRate?>> GetLatest();
        CurrencyRate? GetLatest(string currencyCode);
    }
}
