using API.Dtos.Media;
using API.Dtos.Product;
using API.Dtos.Review;
using API.Models.ServiceResponse;

namespace API.Service.Product
{
    public interface IProductService
    {
        Task<ServiceResponse<List<GetProductDto>>> GetAllProduct();
        Task<ServiceResponse<GetProductDto>> GetProductById(int id);
        Task<ServiceResponse<GetProductDto>> AddProduct(AddProductDto newProduct);
        Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updateProduct);
        Task<ServiceResponse<GetReviewDto>> AddReview(AddReviewDto newReview);
        Task<ServiceResponse<GetProductDto>> DeleteProduct(int id);
        Task<ServiceResponse<GetMediaDto>> UpdateMedia(UpdateMediaDto updateMedia, int id);
        Task<ServiceResponse<GetMediaDto>> AddMedia(AddMediaDto newMedia);
        Task<ServiceResponse<List<GetProductDto>>> GetProductByName(string name);
        Task<ServiceResponse<List<GetProductDto>>> GetAllProductByCategory(string categoryName);
    }
}