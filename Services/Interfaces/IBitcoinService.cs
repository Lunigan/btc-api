using Btc.Api.DTOs;

namespace Btc.Api.Services.Interfaces
{
    public interface IBitcoinService
    {
        bool CheckForUpdates(DateTime lastTimestamp);
        Task DeleteMany(List<int> ids); 
        Task FetchCoinDeskRateAsync();
        Task<List<BitcoinRateRecordDTO>> GetLatestRecords();
        List<BitcoinRateRecordDTO> GetRecordsFromLastThreeDays();
        List<BitcoinRateRecordSnapshotDTO> GetSnapshots();
        Task SaveRecord(BitcoinRateRecordSnapshotDTO bitcoinRate);
    }
}
