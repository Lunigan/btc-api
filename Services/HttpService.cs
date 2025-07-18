using Btc.Api.Services.Interfaces;
using Newtonsoft.Json;
using System.Web;

namespace Btc.Api.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T?> GetAsync<T>(string uri) where T : class
        {
            if (string.IsNullOrWhiteSpace(uri)) throw new ArgumentNullException(nameof(uri));

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(uri);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var parsedContent = JsonConvert.DeserializeObject<T>(content);

            return parsedContent;
        }

        public async Task<T?> GetAsync<T>(string uri, Dictionary<string, string> queryParams) where T : class
        {
            if (string.IsNullOrWhiteSpace(uri)) throw new ArgumentNullException(nameof(uri));

            if (queryParams == null)
            {
                //log warning
                return await GetAsync<T>(uri);
            }

            var query = GetQuery(queryParams);

            return await GetAsync<T>($"{uri}?{query}");
        }

        private string GetQuery(Dictionary<string, string> queryParams)
        {
            if (queryParams == null)
            {
                //log warning
                return string.Empty;
            }

            var query = string.Join(
                "&",
                queryParams.Select(
                    kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"
                )
            );

            return query;
        }
    }
}
