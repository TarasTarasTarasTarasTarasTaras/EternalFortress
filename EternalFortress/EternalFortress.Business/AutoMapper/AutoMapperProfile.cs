using AutoMapper;
using EternalFortress.Data.EF.Entities;
using EternalFortress.Entities.DTOs;

namespace EternalFortress.Business.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<CountryDTO, Country>().ReverseMap();
        }
    }
}
