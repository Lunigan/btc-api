using Btc.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Btc.Api.Contexts
{
    public class CurrencyDbContext : DbContext
    {
        public DbSet<BitcoinRateRecord> BitcoinRates { get; set; }
        public DbSet<CnbCurrencyRate> CnbRates { get; set; }

        public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options)
            : base(options)
        {
        }
    }

}
