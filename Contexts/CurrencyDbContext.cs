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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BitcoinRateRecord>()
                .Property(x => x.BtcEur)
                .HasPrecision(18, 6);

            modelBuilder.Entity<BitcoinRateRecord>()
                .Property(x => x.DailyChangePercent)
                .HasPrecision(18, 6);

            modelBuilder.Entity<BitcoinRateRecord>()
                .Property(x => x.EurCzk)
                .HasPrecision(18, 6);

            modelBuilder.Entity<CnbCurrencyRate>()
                .Property(r => r.Rate)
                .HasPrecision(18, 6);

            modelBuilder.Entity<CnbCurrencyRate>()
               .HasIndex(x => new { x.CurrencyCode, x.ValidFor })
               .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }

}
