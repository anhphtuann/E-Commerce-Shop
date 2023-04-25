using API.Dtos.Products;
using API.Dtos.Reviews;
using API.Models.Products;
using API.Models.Reviews;
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
            
            CreateMap<Stock, GetProductByIdDto>()
                .ForMember(dest => dest.productName, opt => opt.MapFrom(src => src.product.productName))
                .ForMember(dest => dest.productDescription, opt => opt.MapFrom(src => src.product.productDescription))
                .ForMember(dest => dest.brand, opt => opt.MapFrom(src => src.product.brand))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.product.status))
                .ForMember(dest => dest.categoryName, opt => opt.MapFrom(src => src.category.categoryName))
                .ForMember(dest => dest.location, opt => opt.MapFrom(src => src.category.location));
            
            CreateMap<Review, GetReviewDto>()
                .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.user.firstName + " " + src.user.lastName))
                .ForMember(dest => dest.rate, opt => opt.MapFrom(src => src.rate))
                .ForMember(dest => dest.comment, opt => opt.MapFrom(src => src.comment))
                .ForMember(dest => dest.date, opt => opt.MapFrom(src => src.date));

            
            CreateMap<Stock, GetProductByCategoryDto>()
                .ForMember(dest => dest.productName, opt => opt.MapFrom(src => src.product.productName))
                .ForMember(dest => dest.productDescription, opt => opt.MapFrom(src => src.product.productDescription))
                .ForMember(dest => dest.brand, opt => opt.MapFrom(src => src.product.brand))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.product.status));

            CreateMap<AddProductDto, Product>();
            CreateMap<AddProductDto, Stock>();
        }
    }
}