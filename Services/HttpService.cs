using Btc.Api.Services.Interfaces;
using Newtonsoft.Json;
using System.Web;

namespace Btc.Api.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HttpService> _logger;

        public HttpService(
            IHttpClientFactory httpClientFactory, 
            ILogger<HttpService> logger
        )
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<T?> GetAsync<T>(string uri) where T : class
        {
            if (string.IsNullOrWhiteSpace(uri)) throw new ArgumentNullException(nameof(uri));
            _logger.LogTrace("Creating GET request.");

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
            _logger.LogTrace("Creating GET request with query params.");

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
                _logger.LogWarning("Given query params dictionary is null.");
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
