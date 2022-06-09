using AutoMapper;
using CleanArchMvc.Application.DTO;
using CleanArchMvc.Application.Products.Commands;

namespace CleanArchMvc.Application.Mapping
{
    public class DTOCommandMappingProfile : Profile
    {
        public DTOCommandMappingProfile()
        {
            CreateMap<ProductDTO, ProductCreateCommand>();
            CreateMap<ProductDTO, ProductUpdateCommand>();
        }
    }
}
