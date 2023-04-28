

using E_Commerce_Shop.Dtos.Category;
using E_Commerce_Shop.Dtos.Reviews;
using E_Commerce_Shop.Dtos.User;

namespace E_Commerce_Shop{
    public class AutoMapperProfile: Profile {
        //product map
        public AutoMapperProfile() {
            CreateMap<CreateProductBodyPostDto, Products>();
            CreateMap<CreateProductBodyPostDto, Category>();
            CreateMap<Products, ResponseProductDto>()
            .ForMember(des => des.Id,
                            act => act.MapFrom(src => src.ProductId));
            CreateMap<BodyUpdateProductDto, Products>();
            CreateMap<BodyUpdateProductDto, Category>();
            CreateMap<BodyPostReviewDto, Review>();
            CreateMap<Products, SearchProductResponseDto>();
            CreateMap<Category, SearchProductResponseDto>();
            CreateMap<Category, ResponseCategory>();

            //user map
            CreateMap<User, UserProfile>();
            CreateMap<User, ResponseUserWithId>();
        }
    }
}