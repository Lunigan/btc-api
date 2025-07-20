namespace Btc.Api.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBitcoinRateRecordRepository BitcoinRates { get; }
        ICurrencyRateRepository CurrencyRates { get; }
        Task<int> SaveAsync();
    }

}
