using Btc.Api.Models;

namespace Btc.Api.Services.Interfaces
{
    public interface IBitcoinService
    {
        Task FetchCoinDeskRateAsync();
    }
}
