using Btc.Api.Contexts;
using Btc.Api.Models;
using Btc.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Btc.Api.Repositories
{
    public class CurrencyRateRepository : ICurrencyRateRepository
    {
        private readonly CurrencyDbContext _currencyDbContext;

        public CurrencyRateRepository(CurrencyDbContext currencyDbContext)
        {
            _currencyDbContext = currencyDbContext;
        }

        public async Task AddAsync(CurrencyRate currencyRate) => await _currencyDbContext.CurrencyRates.AddAsync(currencyRate);

        public List<CurrencyRate> GetAll() => [.. _currencyDbContext.CurrencyRates];
        public List<CurrencyRate> GetAll(string currencyCode) => [.. _currencyDbContext.CurrencyRates
            .Where(x => x.SourceCurrencyCode.Equals(currencyCode))
        ];

        public CurrencyRate? Get(int id) => _currencyDbContext.CurrencyRates.FirstOrDefault(x => x.Id == id);

        public CurrencyRate? GetByDate(DateTime date, string currencyCode) => _currencyDbContext.CurrencyRates
            .FirstOrDefault(x => x.ValidFor.Date == date.Date && x.SourceCurrencyCode.Equals(currencyCode));

        public async Task<List<CurrencyRate>> GetLatest()
        {
            var today = DateTime.Today;

            var todayRates = await _currencyDbContext.CurrencyRates
                .Where(r => r.ValidFor.Date == today)
                .ToListAsync();

            if (todayRates.Count > 0) return todayRates;

            // Get the latest available date in the database
            var latestDate = await _currencyDbContext.CurrencyRates
                .MaxAsync(r => r.ValidFor.Date);

            var latestRates = await _currencyDbContext.CurrencyRates
                .Where(r => r.ValidFor.Date == latestDate)
                .ToListAsync();

            return latestRates;
        }

        public CurrencyRate? GetLatest(string currencyCode) => _currencyDbContext.CurrencyRates
            .Where(x => x.SourceCurrencyCode.Equals(currencyCode))
            .OrderByDescending(x => x.ValidFor)
            .FirstOrDefault();
    }
}
