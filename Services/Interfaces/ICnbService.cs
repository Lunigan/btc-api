using Btc.Api.Models;

namespace Btc.Api.Services.Interfaces
{
    public interface ICnbService
    {
        Task<CnbCurrencyRate> FetchCnbRateAsync(DateTime date);
    }
}
