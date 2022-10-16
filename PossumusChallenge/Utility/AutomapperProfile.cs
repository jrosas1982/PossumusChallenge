using AutoMapper;
using Core.Application.Dto;
using Core.Domain.AggregatesModel.Cinema;

namespace API.PossumusChallenge.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CinemaDto, Cinema>().ReverseMap();
            CreateMap<CinemaRoomDto, CinemaRoom>().ReverseMap();
        }
    }
}
