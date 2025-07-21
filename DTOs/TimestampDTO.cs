using Newtonsoft.Json;

namespace Btc.Api.DTOs
{
    public class TimestampDTO
    {
        [JsonProperty("lastTimestamp")]
        public DateTime LastTimestamp { get; set; }
    }
}
