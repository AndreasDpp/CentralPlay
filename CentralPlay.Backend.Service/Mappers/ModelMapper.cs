using AutoMapper;
using CentralPlay.Backend.Service.DTO;

namespace CentralPlay.Backend.Service.Mappers
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<Repository.Domain.Entities.Model, ModelDTO>().ReverseMap();
        }
    }
}
