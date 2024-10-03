using AutoMapper;
using MinimizeApi.Models.Dtos;
using MinimizeApi.Models.Entities;

namespace MinimizeApi.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddRequestDTO, Product>();
            CreateMap<UpdateRequestDTO, Product>();
            CreateMap<Product, ResponseDTO>();


            CreateMap<RegisterDTO, User>();


        }
    }
}
