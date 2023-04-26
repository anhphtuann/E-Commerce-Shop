using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Shop.Data;
using E_Commerce_Shop.Dtos.Products;
using E_Commerce_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Shop.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ShopContext _context;
        private readonly IMapper _mapper;

        public ProductService(ShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateProduct(CreateProductBodyPostDto data)
        {
            Category? category = new Category();
            Products? product = new Products();
            category = await _context.Category.FirstOrDefaultAsync(u => u.Id == data.Id);
            if(category is not null){
                return false;
            }
            var categoryData = _mapper.Map<Category>(data);
            var productData = _mapper.Map<Products>(data);
            await _context.Category.AddAsync(categoryData);
            await _context.SaveChangesAsync();
            var id = _context.Category.Max(x => x.Id);
                     
            productData.CategoryId = id;
            await _context.Product.AddAsync(productData);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<List<Products>> CreateReviewForProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<ResponseProductDto>>> GetAll(string category)
        {
            ServiceResponse<List<ResponseProductDto>> response = new ServiceResponse<List<ResponseProductDto>>();
            List<Products> products = await _context.Product.ToListAsync();
            List<Category> categories = await _context.Category.ToListAsync();
            var productCategoryQuery = (from p in products
                                       join c in categories
                                       on p.CategoryId equals c.Id
                                       where c.CateName == category
                                       select new ResponseProductDto {
                                                   Id = p.ProductId,
                                                   Brand = p.Brand,
                                                   Description = p.Description,
                                                   Name = p.Name,
                                                   Price = p.Price,
                                                   Quantity = p.Quantity,
                                                   Rating = p.Rating,
                                                   Status = p.Status}).ToList();
            try{
                if(productCategoryQuery.Count > 0){
                    response.Data = productCategoryQuery;
                    response.Message = "Get Product Success";
                    response.Success = true;
                }
                else {
                    throw new Exception($"Product with {category} not found");
                }
            }catch(Exception ex){
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
            
        }

        public async Task<ServiceResponse<ResponseProductDto>> GetProductById(int id)
        {
            ServiceResponse<ResponseProductDto> sp = new ServiceResponse<ResponseProductDto>();
            try{
                ResponseProductDto? p = await _context.Product.Where(p => p.ProductId == id)
                                .Select(p => _mapper.Map<ResponseProductDto>(p)).FirstOrDefaultAsync()!;
                if(p is null){
                    throw new Exception($"Product by {id} not found");
                }
                sp.Data = p;
                sp.Message = $"Get product by {id} success";
            }
            catch(Exception ex){
                sp.Message = ex.Message;
                sp.Success = false;
            }
            return sp;            
        }

        public Task<List<Products>> UpdateProductById(int id)
        {
            throw new NotImplementedException();
        }
    }
}