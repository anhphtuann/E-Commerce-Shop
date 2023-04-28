using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using E_Commerce_Shop.Data;
using E_Commerce_Shop.Dtos.Category;
using E_Commerce_Shop.Dtos.Products;
using E_Commerce_Shop.Dtos.Reviews;
using E_Commerce_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Shop.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ShopContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(ShopContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);
        public async Task<bool> CreateProduct(CreateProductBodyPostDto data)
        {
            Category? category = new Category();
            Products? product = new Products();
            category = await _context.Category.FirstOrDefaultAsync(u => u.CateName == data.CateName);
            if(category is not null){
                var productData = _mapper.Map<Products>(data);
                var id = _context.Category.Max(x => x.Id);
                productData.CategoryId = id;
                await _context.Product.AddAsync(productData);
                await _context.SaveChangesAsync();
            } else {
                var productData = _mapper.Map<Products>(data);
                var categoryData = _mapper.Map<Category>(data);
                await _context.Category.AddAsync(categoryData);
                await _context.SaveChangesAsync();
                var id = _context.Category.Max(x => x.Id);
                productData.CategoryId = id;
                await _context.Product.AddAsync(productData);
                await _context.SaveChangesAsync();
            }
            return true;
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

        public async Task<ServiceResponse<bool>>UpdateProductById(int id, BodyUpdateProductDto body)
        {
            ServiceResponse<bool> s = new ServiceResponse<bool>();
            try{
                Products? p = await _context.Product.FirstOrDefaultAsync(c => c.ProductId == id)!;
                if(p is null){
                    throw new Exception($"Product with {id} not found");
                }
                else {
                    s.Message = $"update product with {id} success";
                    s.Data = true;
                }
                p.Name = body.Name;
                p.Description = body.Description;
                p.Status = body.Status;
                p.Price = body.Price;
                p.Quantity = body.Quantity; 
                p.Brand = body.Brand;
                p.Rating = body.Rating;
                await _context.SaveChangesAsync();
            }catch(Exception ex){
                s.Data = false;
                s.Message = ex.Message;
                s.Success = false;
            }
            return s;
        }
        public async Task<ServiceResponse<Review>> CreateReviewForProduct(int id, BodyPostReviewDto body) {
            ServiceResponse<Review> s = new ServiceResponse<Review>();
            try{
                var p = await _context.Product.FirstOrDefaultAsync(p => p.ProductId == id);
                Review r = new Review();
                if(p is null){
                    throw new Exception($"Add review with {id} fail");
                }
                r = _mapper.Map<Review>(body);
                r.ProductForeignId = id;
                await _context.Review.AddAsync(r);
                s.Data = r;
                s.Message = $"Add review with {id} success";
                await _context.SaveChangesAsync();
            }
            catch(Exception ex) {
                s.Message = ex.Message;
                s.Success = false;
            }
            return s;
        }
        public async Task<bool> DeleteProducById(int id) {
            try{
                var p = await _context.Product.FirstOrDefaultAsync(p => p.ProductId == id);
                if(p is null){
                    return false;
                }
                _context.Product.Remove(p);
                await _context.SaveChangesAsync();
                return true;
            }finally{

            }
        }
        public async Task<ServiceResponse<List<SearchProductResponseDto>>> SearchProductByKeyWord(string keyword){
            ServiceResponse<List<SearchProductResponseDto>> s = new ServiceResponse<List<SearchProductResponseDto>>();
            
            try {
                var data = (from p in _context.Product
                        join c in _context.Category
                        on p.CategoryId equals c.Id
                        where p.Name.Contains(keyword)
                        select new SearchProductResponseDto {Name = p.Name,
                                    Description = p.Description,
                                    Status = p.Status,
                                    Price = p.Price,
                                    Quantity = p.Quantity,
                                    Brand = p.Brand,
                                    Rating = p.Rating,
                                    CateName = c.CateName,
                                    Location = c.Location
                                    });
                if(data is null){
                    throw new Exception($"Product with {keyword} not found");
                }
                s.Data = await data.ToListAsync();
            }catch(Exception ex){
                s.Data = null;
                s.Message = ex.Message;
                s.Success = false;
            }
            return s;
        }
        public async Task<ServiceResponse<List<ResponseCategory>>> GetAllCategories() {
            ServiceResponse<List<ResponseCategory>> s = new ServiceResponse<List<ResponseCategory>>();
            try{
                var data = await _context.Category.Select(c => _mapper.Map<ResponseCategory>(c)).ToListAsync();
                if(data.Count == 0) {
                    throw new Exception("Category not found");
                }
                s.Data = data;
                s.Message = "Get category success";
                s.Success = true;

            }catch(Exception ex) {
                s.Message = ex.Message;
            }
            return s;
        }
    }
}