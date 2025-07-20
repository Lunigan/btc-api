//using Btc.Api.Models;
//using Btc.Api.Repositories.Interfaces;
//using Btc.Api.Services.Interfaces;

//namespace Btc.Api.Services
//{
//    public class CurrencySyncService : ICurrencySyncService
//    {
//        private readonly IBitcoinService _bitcoinService;
//        private readonly ICnbService _cnbService;
//        private readonly IHttpService _httpService;
//        private readonly IUnitOfWork _unitOfWork;

//        public CurrencySyncService(
//            IBitcoinService bitcoinService,
//            ICnbService cnbService,
//            IHttpService httpService,
//            IUnitOfWork unitOfWork
//        )
//        {
//            _bitcoinService = bitcoinService;
//            _cnbService = cnbService;
//            _httpService = httpService;
//            _unitOfWork = unitOfWork;
//        }

//        public async Task SyncRatesAsync()
//        {
//            var today = DateTime.UtcNow.Date;

//            var cnbRate = await _cnbService.FetchCnbRateAsync(today);

//            if (cnbRate != null)
//            {
//                await _unitOfWork.CnbRates.AddAsync(cnbRate);
//                await _unitOfWork.SaveAsync();
//            }

//            // Fetch CoinDesk rate
//            var btcRate = await _bitcoinService.FetchCoinDeskRateAsync();
//            if (btcRate != null)
//            {
//                var record = new BitcoinRateRecord
//                {
//                    Timestamp = btcRate.Timestamp,
//                    BtcEur = btcRate.BtcEur,
//                    EurCzk = cnbRate.Rate,
//                };

//                await _unitOfWork.BitcoinRates.AddAsync(record);
//                await _unitOfWork.SaveAsync();
//            }
//        }
//    }
//}
