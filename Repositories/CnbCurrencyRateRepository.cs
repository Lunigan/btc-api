using Btc.Api.Contexts;
using Btc.Api.Models;
using Btc.Api.Repositories.Interfaces;

namespace Btc.Api.Repositories
{
    public class CnbCurrencyRateRepository : ICnbCurrencyRateRepository
    {
        private readonly CurrencyDbContext _currencyDbContext;

        public CnbCurrencyRateRepository(CurrencyDbContext currencyDbContext)
        {
            _currencyDbContext = currencyDbContext;
        }

        public async Task AddAsync(CnbCurrencyRate currencyRate) => await _currencyDbContext.CnbRates.AddAsync(currencyRate);

        public List<CnbCurrencyRate> Get() => [.. _currencyDbContext.CnbRates];

        public CnbCurrencyRate? Get(int id) => _currencyDbContext.CnbRates.FirstOrDefault(x => x.Id == id);

        public CnbCurrencyRate? GetByDate(DateTime date) => _currencyDbContext.CnbRates.FirstOrDefault(x => x.ValidFor.Date == date.Date);

        public CnbCurrencyRate? GetLatest() => _currencyDbContext.CnbRates
            .OrderByDescending(x => x.Id)
            .FirstOrDefault();
    }
}
