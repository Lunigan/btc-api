namespace Btc.Api.Models
{
    public class CnbCurrencyRate
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public int Amount { get; set; } = 1;
        public DateTime ValidFor { get; set; }
    }
}
