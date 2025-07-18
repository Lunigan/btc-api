using Newtonsoft.Json;

namespace Btc.Api.DTOs.CoinDesk
{
    public class CoinDeskInstrumentResponse
    {
        [JsonProperty("PRICE")]
        public decimal Price { get; set; }
    }
}
