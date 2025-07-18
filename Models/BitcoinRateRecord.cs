﻿namespace Btc.Api.Models
{
    public class BitcoinRateRecord
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal BtcEur { get; set; }
        public decimal EurCzk { get; set; }
        public decimal BtcCzk => BtcEur * EurCzk;
    }
}
