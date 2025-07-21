using Newtonsoft.Json;

namespace Btc.Api.DTOs
{
    public class CurrencyRateDTO
    {
        [JsonProperty("sourceCurrencyCode")]
        public string SourceCurrencyCode { get; set; } = string.Empty;

        [JsonProperty("targetCurrencyCode")]
        public string TargetCurrencyCode { get; set; } = "CZK";

        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; } = 1;

        [JsonProperty("validFor")]
        public DateTime ValidFor { get; set; }
    }
}
