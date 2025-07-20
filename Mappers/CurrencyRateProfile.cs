using AutoMapper;
using Btc.Api.DTOs.Responses;
using Btc.Api.Models;

namespace Btc.Api.Mappers
{
    public class CurrencyRateProfile : Profile
    {
        public CurrencyRateProfile()
        {
            CreateMap<CurrencyRate, CurrencyRateResponse>()
                .ForMember(dest => dest.SourceCurrencyCode, opt => opt.MapFrom(src => src.SourceCurrencyCode))
                .ForMember(dest => dest.TargetCurrencyCode, opt => opt.MapFrom(src => src.TargetCurrencyCode))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.ValidFor, opt => opt.MapFrom(src => src.ValidFor));
        }
    }
}
