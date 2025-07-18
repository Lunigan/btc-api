using Newtonsoft.Json;

namespace Btc.Api.DTOs.Cnb
{
    public class CnbRootResponse
    {
        [JsonProperty("rates")]
        public List<CnbCurrencyRateResponse> Rates { get; set; } = [];
    }
}
