using AutoMapper;
using EternalFortress.API.Models;
using EternalFortress.Data.EF.Entities;
using EternalFortress.Entities.DTOs;

namespace EternalFortress.API.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, UserDTO>().ReverseMap();

            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<CountryDTO, Country>().ReverseMap();
        }
    }
}
