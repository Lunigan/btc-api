using Btc.Api.Models;

namespace Btc.Api.Repositories.Interfaces
{
    public interface IBitcoinRateRecordSnapshotRepository
    {
        Task AddAsync(BitcoinRateRecordSnapshot snapshot);
        List<BitcoinRateRecordSnapshot> GetAll();
        Task RemoveMany(List<int> ids);
        void Update(BitcoinRateRecordSnapshot snapshot);
    }
}
