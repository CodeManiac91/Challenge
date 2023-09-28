using AutoMapper;
using PowerplantCodingChallenge.API.Dtos;

namespace PowerplantCodingChallenge.API.Models
{
    public class PowerplanCalculationProfile : Profile
    {
        public PowerplanCalculationProfile()
        {
            CreateMap<GenerationDetailsDto, GenerationDetails>();
            CreateMap<ForecastDto, Forecast>();
            CreateMap<Dtos.PowerPlantType, PowerPlantType>().ReverseMap();
            CreateMap<PowerPlantDto, PowerPlant>();
            CreateMap<GenerationPlan, GenerationPlanDto>();
        }
    }
}
