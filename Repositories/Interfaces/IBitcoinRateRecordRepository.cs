using Btc.Api.Models;

namespace Btc.Api.Repositories.Interfaces
{
    public interface IBitcoinRateRecordRepository
    {
        Task AddAsync(BitcoinRateRecord record);
        List<BitcoinRateRecord> Get();
        BitcoinRateRecord? Get(int id);
        List<BitcoinRateRecord> GetLatest();
        BitcoinRateRecord? GetLatestRecord();
        List<BitcoinRateRecord> GetRecordsFromLastDays(int i);
    }
}
