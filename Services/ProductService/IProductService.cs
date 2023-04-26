using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Shop.Dtos.Products;
using E_Commerce_Shop.Models;

namespace E_Commerce_Shop.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<ResponseProductDto>>> GetAll(string category);
        Task<bool> CreateProduct(CreateProductBodyPostDto data);
        Task<ServiceResponse<ResponseProductDto>> GetProductById(int id);
        Task<List<Products>> UpdateProductById(int id);
        Task<List<Products>> CreateReviewForProduct(int id);
    }
}