using Btc.Api.Models;

namespace Btc.Api.Repositories.Interfaces
{
    public interface ICnbCurrencyRateRepository
    {
        Task AddAsync(CnbCurrencyRate currencyRate);
        List<CnbCurrencyRate> Get();
        CnbCurrencyRate? Get(int id);
        CnbCurrencyRate? GetByDate(DateTime date);
        CnbCurrencyRate? GetLatest();
    }
}
