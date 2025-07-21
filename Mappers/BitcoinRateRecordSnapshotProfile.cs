using AutoMapper;
using Btc.Api.DTOs;
using Btc.Api.Models;

namespace Btc.Api.Mappers
{
    public class BitcoinRateRecordSnapshotProfile : Profile
    {
        public BitcoinRateRecordSnapshotProfile()
        {
            CreateMap<BitcoinRateRecordSnapshot, BitcoinRateRecordSnapshotDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Instrument, opt => opt.MapFrom(src => src.Instrument))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp))
                .ForMember(dest => dest.BtcEurPrice, opt => opt.MapFrom(src => src.BtcEur))
                .ForMember(dest => dest.Eur2Czk, opt => opt.MapFrom(src => src.EurCzk))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
                .ReverseMap();
        }
    }
}
