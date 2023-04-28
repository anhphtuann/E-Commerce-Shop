using API.Dtos.Carts;
using API.Dtos.Products;
using API.Dtos.Reviews;
using API.Dtos.Users;
using API.Models.Carts;
using API.Models.Products;
using API.Models.Reviews;
using API.Models.Users;
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

            CreateMap<AddReviewDto, Review>();

            CreateMap<User, GetUserDto>()
                .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => src.firstName))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.lastName))
                .ForMember(dest => dest.account, opt => opt.MapFrom(src => src.account))
                .ForMember(dest => dest.role, opt => opt.MapFrom(src => src.role))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.contact, opt => opt.MapFrom(src => src.contact));
            
            CreateMap<User, GetAllUserDto>()
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.userId))
                .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => src.firstName))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.lastName))
                .ForMember(dest => dest.account, opt => opt.MapFrom(src => src.account))
                .ForMember(dest => dest.role, opt => opt.MapFrom(src => src.role))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.contact, opt => opt.MapFrom(src => src.contact));
                

            CreateMap<CartProduct, GetMyCartProductDto>()
                .ForMember(dest => dest.productName, opt => opt.MapFrom(src => src.product.productName))
                .ForMember(dest => dest.priceUnit, opt => opt.MapFrom(src => src.priceUnit))
                .ForMember(dest => dest.quantity, opt => opt.MapFrom(src => src.quantity))
                .ForMember(dest => dest.totalProductPrice, opt => opt.MapFrom(src => src.totalProductPrice));

            CreateMap<Cart, GetMyCartDto>()
                .ForMember(dest => dest.totalPrice, opt => opt.MapFrom(src => src.totalPrice))
                .ForMember(dest => dest.products, opt => opt.MapFrom(src => src.products));

            CreateMap<Cart, GetCartDto>()
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.userId))
                .ForMember(dest => dest.totalPrice, opt => opt.MapFrom(src => src.totalPrice));

            CreateMap<CartProduct, GetCartProductDto>()
                .ForMember(dest => dest.proId, opt => opt.MapFrom(src => src.proId))
                .ForMember(dest => dest.productName, opt => opt.MapFrom(src => src.product.productName))
                .ForMember(dest => dest.priceUnit, opt => opt.MapFrom(src => src.priceUnit))
                .ForMember(dest => dest.quantity, opt => opt.MapFrom(src => src.quantity))
                .ForMember(dest => dest.totalProductPrice, opt => opt.MapFrom(src => src.totalProductPrice)); 
        
            CreateMap<AddCartDto, Cart>();

            CreateMap<AddCartProductDto, CartProduct>();
        }
    }
}