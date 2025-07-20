using Btc.Api.Services.Interfaces;

namespace Btc.Api.Services.BackgroundServices
{
    public class CoinDeskPollingService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public CoinDeskPollingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// This methods gets new data from CoinDesk API every 10 minutes
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("CoinDeskPollingService is running...");

            var timer = new PeriodicTimer(TimeSpan.FromMinutes(10));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var yourService = scope.ServiceProvider.GetRequiredService<IBitcoinService>();
                    await yourService.FetchCoinDeskRateAsync();
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex, "Error occurred while polling API.");
                }
            }
        }
    }
}
