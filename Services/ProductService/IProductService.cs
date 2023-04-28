using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Shop.Dtos.Category;
using E_Commerce_Shop.Dtos.Products;
using E_Commerce_Shop.Dtos.Reviews;
using E_Commerce_Shop.Models;

namespace E_Commerce_Shop.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<ResponseProductDto>>> GetAll(string category);
        Task<bool> CreateProduct(CreateProductBodyPostDto data);
        Task<ServiceResponse<ResponseProductDto>> GetProductById(int id);
        Task<ServiceResponse<bool>> UpdateProductById(int id, BodyUpdateProductDto body);
        Task<ServiceResponse<Review>> CreateReviewForProduct(int id, BodyPostReviewDto body);
        Task<bool> DeleteProducById(int id);
        Task<ServiceResponse<List<SearchProductResponseDto>>> SearchProductByKeyWord(string keyword);
        Task<ServiceResponse<List<ResponseCategory>>> GetAllCategories();
    }
}