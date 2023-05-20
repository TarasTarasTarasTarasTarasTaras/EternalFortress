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
            CreateMap<ChunkModel, ChunkDTO>().ReverseMap();
            CreateMap<RegisterModel, UserDTO>().ReverseMap();
            CreateMap<FolderModel, FolderDTO>().ReverseMap();
            CreateMap<FileInfoModel, FileInfoDTO>().ReverseMap();

            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<FolderDTO, Folder>().ReverseMap();
            CreateMap<CountryDTO, Country>().ReverseMap();
            CreateMap<FileInfoDTO, Data.EF.Entities.FileInfo>().ReverseMap();
        }
    }
}
