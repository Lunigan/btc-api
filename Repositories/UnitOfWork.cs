using Btc.Api.Contexts;
using Btc.Api.Repositories.Interfaces;

namespace Btc.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CurrencyDbContext _context;

        public IBitcoinRateRecordRepository BitcoinRates { get; }
        public ICnbCurrencyRateRepository CnbRates { get; }

        public UnitOfWork(
            CurrencyDbContext context,
            IBitcoinRateRecordRepository bitcoinRates,
            ICnbCurrencyRateRepository cnbRates
        )
        {
            _context = context;
            BitcoinRates = bitcoinRates;
            CnbRates = cnbRates;
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
