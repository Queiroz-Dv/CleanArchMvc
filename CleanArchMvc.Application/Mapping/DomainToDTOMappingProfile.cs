using AutoMapper;
using CleanArchMvc.Application.DTO;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Mapping
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
