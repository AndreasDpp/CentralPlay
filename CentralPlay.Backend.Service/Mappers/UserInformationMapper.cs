using AutoMapper;
using CentralPlay.Backend.Service.DTO;

namespace CentralPlay.Backend.Service.Mappers
{
    public class UserInformationMapper : Profile
    {
        public UserInformationMapper()
        {
            CreateMap<Repository.Domain.Entities.User, UserDTO>().ReverseMap();
        }
    }
}
