using Btc.Api.Contexts;
using Btc.Api.Models;
using Btc.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Btc.Api.Repositories
{
    public class BitcoinRateRecordSnapshotRepository : IBitcoinRateRecordSnapshotRepository
    {
        private readonly CurrencyDbContext _currencyDbContext;

        public BitcoinRateRecordSnapshotRepository(CurrencyDbContext currencyDbContext)
        {
            _currencyDbContext = currencyDbContext;            
        }

        public async Task AddAsync(BitcoinRateRecordSnapshot snapshot) => await _currencyDbContext.BitcoinRateRecordSnapshots
            .AddAsync(snapshot);

        public List<BitcoinRateRecordSnapshot> GetAll() => [.. _currencyDbContext.BitcoinRateRecordSnapshots];

        public async Task RemoveMany(List<int> ids)
        {
            if (ids == null || ids.Count < 1) return;

            var recordsToDelet = await _currencyDbContext.BitcoinRateRecordSnapshots
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            _currencyDbContext.BitcoinRateRecordSnapshots.RemoveRange(recordsToDelet);
        }

        public void Update(BitcoinRateRecordSnapshot snapshot) => _currencyDbContext.BitcoinRateRecordSnapshots.Update(snapshot);
    }
}
