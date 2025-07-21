using AutoMapper;
using Btc.Api.DTOs;
using Btc.Api.DTOs.CoinDesk;
using Btc.Api.Models;
using Btc.Api.Repositories.Interfaces;
using Btc.Api.Services.Interfaces;

namespace Btc.Api.Services
{
    public class BitcoinService : IBitcoinService
    {
        private readonly IHttpService _httpService;
        private readonly ILogger<BitcoinService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private const string COIN_DESK_BTC_EUR_API_URI = "https://data-api.coindesk.com/spot/v1/latest/tick?market=coinbase&instruments=BTC-EUR";

        public BitcoinService(
            IHttpService httpService,
            ILogger<BitcoinService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork
        )
        {
            _httpService = httpService;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public bool CheckForUpdates(DateTime lastTimestamp)
        {
            _logger.LogInformation("Checking if updates are available.");

            var latest = _unitOfWork.BitcoinRates.GetLatestRecord();

            if (latest != null && latest.Timestamp.ToString("s") != lastTimestamp.ToString("s")) return true;

            return false;
        }

        public async Task DeleteMany(List<int> ids)
        {
            _logger.LogInformation($"Starting DeleteMany for {ids.Count} enetities.");
            await _unitOfWork.BitcoinRateSnapshots.RemoveMany(ids);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation($"Deleted {ids.Count} enetities.");
        }

        public async Task FetchCoinDeskRateAsync()
        {
            _logger.LogInformation("Fetching data from CoinDesk API");
            var response = await _httpService.GetAsync<CoinDeskRootResponse>(COIN_DESK_BTC_EUR_API_URI);

            if (response?.Data.TryGetValue("BTC-EUR", out var instrument) == true)
            {
                var eurCzk = _unitOfWork.CurrencyRates.GetLatest("EUR");
                await _unitOfWork.BitcoinRates.AddAsync(new()
                {
                    BtcEur = instrument.Price,
                    EurCzk = eurCzk?.Rate ?? 0M,
                    Timestamp = DateTime.UtcNow
                });
                await _unitOfWork.SaveAsync();
            }
            else
            {
                _logger.LogError("No relevant CoinDesk data fro BTC-EUR.");
            }

        }

        public async Task<List<BitcoinRateRecordDTO>> GetLatestRecords()
        {
            _logger.LogInformation("Getting latest records from last 24h.");
            var latest = _unitOfWork.BitcoinRates.GetLatest();
            return latest != null 
                ? [.. latest.Select(x => _mapper.Map<BitcoinRateRecordDTO>(x))]
                : [];
        }

        public List<BitcoinRateRecordDTO> GetRecordsFromLastThreeDays()
        {
            _logger.LogInformation("Getting latest records from last 72h.");
            var latest = _unitOfWork.BitcoinRates.GetRecordsFromLastDays(3);
            return latest != null
                ? [.. latest.Select(x => _mapper.Map<BitcoinRateRecordDTO>(x))]
                : [];
        }

        public List<BitcoinRateRecordSnapshotDTO> GetSnapshots()
        {
            _logger.LogInformation("Getting all snapshots.");
            var snapshots = _unitOfWork.BitcoinRateSnapshots.GetAll();
            return snapshots != null
                ? [.. snapshots.Select(x => _mapper.Map<BitcoinRateRecordSnapshotDTO>(x))]
                : [];
        }

        public async Task SaveRecord(BitcoinRateRecordSnapshotDTO bitcoinRate)
        {
            _logger.LogInformation("Saving snapshot.");
            var model = _mapper.Map<BitcoinRateRecordSnapshot>(bitcoinRate);

            if (model.Id > 0)
            {
                _logger.LogInformation("Updating snapshot.");
                _unitOfWork.BitcoinRateSnapshots.Update(model);
            }
            else
            {
                _logger.LogInformation("Creating snapshot.");
                await _unitOfWork.BitcoinRateSnapshots.AddAsync(model);
            }
                
            await _unitOfWork.SaveAsync();
        }
    }
}
