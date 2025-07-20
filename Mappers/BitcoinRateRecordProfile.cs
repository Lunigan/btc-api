using AutoMapper;
using Btc.Api.DTOs.Responses;
using Btc.Api.Models;

namespace Btc.Api.Mappers
{
    public class BitcoinRateRecordProfile : Profile
    {
        public BitcoinRateRecordProfile()
        {
            CreateMap<BitcoinRateRecord, BitcoinRateRecordResponse>()
                .ForMember(dest => dest.Instrument, opt => opt.MapFrom(src => src.Instrument))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp))
                .ForMember(dest => dest.BtcEurPrice, opt => opt.MapFrom(src => src.BtcEur))
                .ForMember(dest => dest.Eur2Czk, opt => opt.MapFrom(src => src.EurCzk));
        }
    }
}
