namespace Btc.Api.Services.Interfaces
{
    public interface IHttpService
    {
        Task<T?> GetAsync<T>(string uri) where T : class;
        Task<T?> GetAsync<T>(string uri, Dictionary<string, string> queryParams) where T : class;
    }
}
