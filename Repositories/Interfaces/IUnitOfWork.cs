namespace Btc.Api.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBitcoinRateRecordRepository BitcoinRates { get; }
        ICnbCurrencyRateRepository CnbRates { get; }
        Task<int> SaveAsync();
    }

}
