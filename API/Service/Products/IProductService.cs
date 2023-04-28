using API.Dtos.Media;
using API.Dtos.Products;
using API.Dtos.Reviews;
using API.Models.ServiceResponse;

namespace API.Service.Products
{
    public interface IProductService
    {
        Task<ServiceResponse<List<GetProductDto>>> GetAllProduct();
        Task<ServiceResponse<GetProductByIdDto>> GetProductById(int id);
        Task<ServiceResponse<GetProductDto>> AddProduct(AddProductDto newProduct);
        Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updateProduct);
        Task<ServiceResponse<GetReviewDto>> AddReview(int id, AddReviewDto newReview);
        Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id);
        Task<ServiceResponse<GetMediaDto>> UpdateMedia(UpdateMediaDto updateMedia, int id);
        Task<ServiceResponse<GetMediaDto>> AddMedia(AddMediaDto newMedia);
        Task<ServiceResponse<List<GetProductDto>>> GetProductByName(string name);
        Task<ServiceResponse<List<GetProductByCategoryDto>>> GetAllProductByCategory(string categoryName);
    }
}