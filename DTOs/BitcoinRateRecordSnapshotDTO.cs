using Newtonsoft.Json;

namespace Btc.Api.DTOs
{
    public class BitcoinRateRecordSnapshotDTO : BitcoinRateRecordDTO
    {
        public BitcoinRateRecordSnapshotDTO() : base()
        {

        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }
    }
}
