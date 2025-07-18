using Btc.Api.Contexts;
using Btc.Api.Models;
using Btc.Api.Repositories.Interfaces;

namespace Btc.Api.Repositories
{
    public class BitcoinRateRecordRepository : IBitcoinRateRecordRepository
    {
        private readonly CurrencyDbContext _currencyDbContext;

        public BitcoinRateRecordRepository(CurrencyDbContext currencyDbContext)
        {
            _currencyDbContext = currencyDbContext;
        }

        public async Task AddAsync(BitcoinRateRecord record) => await _currencyDbContext.BitcoinRates.AddAsync(record);

        public BitcoinRateRecord? Get(int id) => _currencyDbContext.BitcoinRates.FirstOrDefault(x => x.Id == id);

        public List<BitcoinRateRecord> Get() => [.. _currencyDbContext.BitcoinRates];
    }
}
