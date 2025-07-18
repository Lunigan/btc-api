using Newtonsoft.Json;

namespace Btc.Api.DTOs.CoinDesk
{
    public class CoinDeskRootResponse
    {
        [JsonProperty("Data")]
        public Dictionary<string, CoinDeskInstrumentResponse> Data { get; set; } = [];
    }
}
