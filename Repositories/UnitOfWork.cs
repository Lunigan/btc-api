using Btc.Api.Contexts;
using Btc.Api.Repositories.Interfaces;

namespace Btc.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CurrencyDbContext _context;

        public IBitcoinRateRecordRepository BitcoinRates { get; }
        public IBitcoinRateRecordSnapshotRepository BitcoinRateSnapshots { get; }
        public ICurrencyRateRepository CurrencyRates { get; }

        public UnitOfWork(
            CurrencyDbContext context,
            IBitcoinRateRecordRepository bitcoinRates,
            IBitcoinRateRecordSnapshotRepository bitcoinRateSnapshots,
            ICurrencyRateRepository currencyRates
        )
        {
            _context = context;
            BitcoinRates = bitcoinRates;
            BitcoinRateSnapshots = bitcoinRateSnapshots;
            CurrencyRates = currencyRates;
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
