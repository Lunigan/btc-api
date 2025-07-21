using Btc.Api.Services.Interfaces;

namespace Btc.Api.Services.BackgroundServices
{
    public class CnbPollingService : BackgroundService
    {
        private readonly ILogger<CnbPollingService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public CnbPollingService(
            ILogger<CnbPollingService> logger,
            IServiceProvider serviceProvider
        )
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// This method executes every dat at 14:45 and attempts get get new data from ČNB API.
        /// Retry policy - 2 more attempts 30 minutes appart.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           _logger.LogInformation("CnbPollingService is running...");

            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var scheduledTime = new DateTime(now.Year, now.Month, now.Day, 14, 45, 0);

                if (now > scheduledTime)
                    scheduledTime = scheduledTime.AddDays(1);

                var delay = scheduledTime - now;
                await Task.Delay(delay, stoppingToken);

                using var scope = _serviceProvider.CreateScope();
                var cnbService = scope.ServiceProvider.GetRequiredService<ICurrencyService>();

                bool success = await cnbService.TryFetchCurrentRates();

                if (!success)
                {
                    _logger.LogWarning("Initial fetch failed. Starting fallback retries...");
                    int retries = 3; // Retry for ~45 minutes
                    for (int i = 0; i < retries && !stoppingToken.IsCancellationRequested; i++)
                    {
                        await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                        success = await cnbService.TryFetchCurrentRates();

                        if (success)
                        {
                            _logger.LogInformation($"Fallback fetch succeeded on attempt {i + 1}.");
                            break;
                        }
                    }

                    if (!success)
                        _logger.LogError("Fallback retries failed. Will try again tomorrow.");
                }
            }
        }
    }
}
