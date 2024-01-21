using StorageAPI.DTO;
using StorageAPI.Models;
using AutoMapper;
namespace StorageAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<UserInfo, UserInfoDTO>();
            CreateMap<RegisterDTO,UserInfo>();
            CreateMap<LoginDto,UserInfo>();
        }
    }
}
