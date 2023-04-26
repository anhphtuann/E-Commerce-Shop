

namespace E_Commerce_Shop{
    public class AutoMapperProfile: Profile {
        public AutoMapperProfile() {
            CreateMap<CreateProductBodyPostDto, Products>();
            CreateMap<CreateProductBodyPostDto, Category>();
            // .ForMember(
            //      des => des.Id,
            //              act => act.MapFrom(src => src.Id)
            //  );
            CreateMap<Products, ResponseProductDto>()
            .ForMember(des => des.Id,
                            act => act.MapFrom(src => src.ProductId));

        }
    }
}