namespace Btc.Api.Models
{
    public class CurrencyRate
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string SourceCurrencyCode { get; set; } = string.Empty;
        public string TargetCurrencyCode { get; set; } = "CZK";
        public decimal Rate { get; set; }
        public int Amount { get; set; } = 1;
        public DateTime ValidFor { get; set; }
        public string Source { get; set; } = "ČNB";
    }
}
