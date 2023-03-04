using AutoMapper;
using Core.Entities;
using Sensedia.API.Dtos;

namespace Sensedia.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(d => d.ProductBrand.Name))
                .ForMember(d => d.ProductType, s => s.MapFrom(d => d.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
                
        }
    }
}
