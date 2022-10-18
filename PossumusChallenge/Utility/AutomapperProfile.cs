using AutoMapper;
using Core.Application.Dto;
using Core.Domain.AggregatesModel.RRHH;

namespace API.PossumusChallenge.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CandidatoActualizacionDto, Candidato>().ReverseMap();
            CreateMap<CandidatoCreacionDto, Candidato>().ReverseMap();
            CreateMap<CandidatoDto, Candidato>().ReverseMap();
            CreateMap<EmpleoActualizacionDto, Empleo>().ReverseMap();
            CreateMap<EmpleoDto, Empleo>().ReverseMap();
        }
    }
}
