using API.Dtos.Product;
using API.Models.Products;
using AutoMapper;

namespace API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Stock, GetProductDto>()
                .ForMember(dest => dest.productName, opt => opt.MapFrom(src => src.product.productName))
                .ForMember(dest => dest.productDescription, opt => opt.MapFrom(src => src.product.productDescription))
                .ForMember(dest => dest.brand, opt => opt.MapFrom(src => src.product.brand))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.product.status))
                .ForMember(dest => dest.categoryName, opt => opt.MapFrom(src => src.category.categoryName))
                .ForMember(dest => dest.location, opt => opt.MapFrom(src => src.category.location));
        }
    }
}