using AutoMapper;
using Btc.Api.DTOs;
using Btc.Api.DTOs.Cnb;
using Btc.Api.Models;
using Btc.Api.Repositories.Interfaces;
using Btc.Api.Services.Interfaces;

namespace Btc.Api.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IHttpService _httpService;
        private readonly ILogger<CurrencyService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private const string CNB_CURRENCY_RAES_API_URI = "https://api.cnb.cz/cnbapi/exrates/daily";

        public CurrencyService(
            IHttpService httpService,
            ILogger<CurrencyService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork
        )
        {
            _httpService = httpService;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CurrencyRateDTO>> GetLatestCurrencyRates()
        {
            _logger.LogInformation("Getting latest currency rates for all available currencies.");
            var latest = await _unitOfWork.CurrencyRates.GetLatest();
            return latest != null ? [.. latest.Select(x => _mapper.Map<CurrencyRateDTO>(x))] : [];
        }

        public CurrencyRateDTO? GetLatestCurrencyRate(string currencyCode)
        {
            _logger.LogInformation("Get latest currency rate for given currency.");
            var latestModel = _unitOfWork.CurrencyRates.GetLatest(currencyCode);
            return latestModel != null ? _mapper.Map<CurrencyRateDTO>(latestModel) : null;
        }

        /// <summary>
        /// This method attempts to get latest currency rates from ČNB API
        /// </summary>
        /// <returns></returns>
        public async Task<bool> TryFetchCurrentRates()
        {
            _logger.LogInformation("Trying to fetch new currency rates from ČNB");
            var today = TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time")
            );

            var query = new Dictionary<string, string> {
                { "date", $"{today.Date:yyyy-MM-dd}" },
                { "lang", "EN" }
            };

            var response = await _httpService.GetAsync<CnbRootResponse>(CNB_CURRENCY_RAES_API_URI, query);

            if (response != null)
            {
                if (response.Rates.Count > 0 && response.Rates[0].ValidFor != today.Date) return false;

                foreach (var item in response.Rates)
                {
                    var currencyRate = new CurrencyRate()
                    {
                        Amount = item.Amount,
                        SourceCurrencyCode = item.CurrencyCode,
                        TargetCurrencyCode = "CZK",
                        Rate = item.Rate,
                        Timestamp = DateTime.UtcNow,
                        ValidFor = item.ValidFor,
                        Source = "ČNB"
                    };

                    await _unitOfWork.CurrencyRates.AddAsync(currencyRate);
                    await _unitOfWork.SaveAsync();
                }
                return true;
            }
            _logger.LogError("Null response from ČNB API.");
            return false;
        }
    }
}
