using Btc.Api.Contexts;
using Btc.Api.Repositories.Interfaces;

namespace Btc.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CurrencyDbContext _context;

        public IBitcoinRateRecordRepository BitcoinRates { get; }
        public ICurrencyRateRepository CurrencyRates { get; }

        public UnitOfWork(
            CurrencyDbContext context,
            IBitcoinRateRecordRepository bitcoinRates,
            ICurrencyRateRepository currencyRates
        )
        {
            _context = context;
            BitcoinRates = bitcoinRates;
            CurrencyRates = currencyRates;
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
