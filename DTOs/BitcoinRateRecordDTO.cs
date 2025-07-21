using Newtonsoft.Json;

namespace Btc.Api.DTOs
{
    public class BitcoinRateRecordDTO
    {
        [JsonProperty("instrument")]
        public string Instrument { get; set; } = "BTC-EUR";

        [JsonProperty("btcEurPrice")]
        public decimal BtcEurPrice { get; set; }

        [JsonProperty("eur2Czk")]
        public decimal Eur2Czk { get; set; }

        [JsonProperty("btcCzkPrice")]
        public decimal BtcCzkPrice => BtcEurPrice * Eur2Czk;

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
