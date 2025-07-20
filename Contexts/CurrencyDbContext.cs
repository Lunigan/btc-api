using Btc.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Btc.Api.Contexts
{
    public class CurrencyDbContext : DbContext
    {
        public DbSet<BitcoinRateRecord> BitcoinRates { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

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
                .Property(x => x.EurCzk)
                .HasPrecision(18, 6);

            modelBuilder.Entity<CurrencyRate>()
                .Property(r => r.Rate)
                .HasPrecision(18, 6);

            modelBuilder.Entity<CurrencyRate>()
               .HasIndex(x => new { x.SourceCurrencyCode, x.ValidFor })
               .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }

}
