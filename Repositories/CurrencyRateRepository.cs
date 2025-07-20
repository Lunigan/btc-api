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
        public List<CurrencyRate> GetAll(string curencyCode) => [.. _currencyDbContext.CurrencyRates
            .Where(x => x.SourceCurrencyCode.Equals(curencyCode))
        ];

        public CurrencyRate? Get(int id) => _currencyDbContext.CurrencyRates.FirstOrDefault(x => x.Id == id);

        public CurrencyRate? GetByDate(DateTime date, string curencyCode) => _currencyDbContext.CurrencyRates
            .FirstOrDefault(x => x.ValidFor.Date == date.Date && x.SourceCurrencyCode.Equals(curencyCode));

        public async Task<List<CurrencyRate?>> GetLatest()
        {
            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);

            // Try today first
            var latestRates = await _currencyDbContext.CurrencyRates
                .Where(r => r.ValidFor.Date == today)
                .GroupBy(r => r.SourceCurrencyCode)
                .Select(g => g
                    .OrderByDescending(r => r.ValidFor)
                    .FirstOrDefault()
                )
                .ToListAsync();

            // If none, try yesterday
            if (!latestRates.Any())
            {
                latestRates = await _currencyDbContext.CurrencyRates
                    .Where(r => r.ValidFor.Date == yesterday)
                    .GroupBy(r => r.SourceCurrencyCode)
                    .Select(g => g
                        .OrderByDescending(r => r.ValidFor)
                        .FirstOrDefault()
                    )
                    .ToListAsync();
            }

            return latestRates;
        }

        public CurrencyRate? GetLatest(string curencyCode) => _currencyDbContext.CurrencyRates
            .OrderByDescending(x => x.Id)
            .Where(x => x.SourceCurrencyCode.Equals(curencyCode))
            .FirstOrDefault();
    }
}
