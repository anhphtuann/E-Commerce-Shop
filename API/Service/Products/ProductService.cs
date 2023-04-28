using System.Security.Claims;
using API.Dtos.Media;
using API.Dtos.Products;
using API.Dtos.Reviews;
using API.Models.Products;
using API.Models.Reviews;
using API.Models.ServiceResponse;
using AutoMapper;

namespace API.Service.Products
{
    public class ProductService : IProductService
    {
        private readonly ShopContext _shopContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(IMapper mapper, ShopContext shopContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _shopContext = shopContext;
            
        }
        public Task<ServiceResponse<GetMediaDto>> AddMedia(AddMediaDto newMedia)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<GetProductDto>> AddProduct(AddProductDto newProduct)
        {
            var response = new ServiceResponse<GetProductDto>();
            try
            {
                var product = _mapper.Map<Product>(newProduct);
                _shopContext.Products.Add(product);
                
                await _shopContext.SaveChangesAsync();

                var latestProId = _shopContext.Products.Max(p => p.proId);

                var stock = _mapper.Map<Stock>(newProduct);
                stock.proId = latestProId;
                stock.vendorUserId = (GetUserRole() == "administrator") ? 0 : GetUserId();
                stock.dateModify = DateTime.Now;
                _shopContext.Stocks.Add(stock);

                await _shopContext.SaveChangesAsync();

                var resultQuery = await _shopContext.Stocks
                                .Include(s => s.product)
                                .Include(s => s.category)
                                .Where(s => s.product.proId == latestProId)
                                .FirstOrDefaultAsync();
                
                var result = _mapper.Map<GetProductDto>(resultQuery);

                response.Data = result;
                                    
                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetReviewDto>> AddReview(int id, AddReviewDto newReview)
        {
            var response = new ServiceResponse<GetReviewDto>();
            try
            {
                var review = _mapper.Map<Review>(newReview);
                review.userId = GetUserId();
                review.proId = id;
                review.date = DateTime.Now;
                _shopContext.Reviews.Add(review);
                
                await _shopContext.SaveChangesAsync();

                var resultQuery = await _shopContext.Reviews
                                    .Include(r => r.user)
                                    .OrderByDescending(r => r.reviewId)
                                    .Where(r => 
                                        (r.userId == GetUserId() && 
                                        r.proId == id))
                                    .FirstOrDefaultAsync();

                var result = _mapper.Map<GetReviewDto>(resultQuery);

                if (result is not null)
                {
                    response.Data = result;
                }
                                    
                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id)
        {
             var response = new ServiceResponse<List<GetProductDto>>();
            try
            {
                var stock = await _shopContext.Stocks
                                .Include(s => s.product)
                                .Where(s => s.vendorUserId == GetUserId() || GetUserRole() == "administrator")
                                .FirstOrDefaultAsync(s => s.proId == id);
                
                var product = stock.product;

                if (stock is null || product is null)
                {
                    throw new Exception($"Product has id {id} not found");
                } 
                
                _shopContext.Products.Remove(product);
                _shopContext.Stocks.Remove(stock);

                await _shopContext.SaveChangesAsync();

                var resultQuery = await _shopContext.Stocks
                                        .Include(s => s.product)
                                        .Include(s => s.category)
                                        .ToListAsync();

                var result = resultQuery
                                .OrderBy(s => s.proId)
                                .Select(s => _mapper.Map<GetProductDto>(s))
                                .ToList();

                if (result is not null)
                {
                    response.Data = result;
                }   

                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProduct()
        {
            var response = new ServiceResponse<List<GetProductDto>>();
            try
            {
                var resultQuery = await _shopContext.Stocks
                                        .Include(s => s.category)
                                        .Include(s => s.product)
                                        .ToListAsync();
                
                if (resultQuery is null)
                {
                    throw new Exception($"Database is empty or something wrong");
                } 
                
                var result = resultQuery
                            .OrderBy(s => s.proId)
                            .Select(s => _mapper.Map<GetProductDto>(s))
                            .ToList();     

                if (result is not null)
                {
                    response.Data = result;
                }   

                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetProductByCategoryDto>>> GetAllProductByCategory(string categoryName)
        {
            var response = new ServiceResponse<List<GetProductByCategoryDto>>();
            try
            {
                var resultQuery = await _shopContext.Stocks
                                        .Include(s => s.category)
                                        .Include(s => s.product)
                                        .Where(s => s.category.categoryName == categoryName)
                                        .ToListAsync();
                
                if (resultQuery is null)
                {
                    throw new Exception($"Category has name {categoryName} empty or not existing");
                } 
                
                var result = resultQuery
                            .Select(s => _mapper.Map<GetProductByCategoryDto>(s))
                            .ToList();     

                if (result is not null)
                {
                    response.Data = result;
                }   

                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetProductByIdDto>> GetProductById(int id)
        {
            var response = new ServiceResponse<GetProductByIdDto>();
            try
            {
                var resultQuery = await _shopContext.Stocks
                                .Include(s => s.category)
                                .Include(s => s.product)
                                .FirstOrDefaultAsync(s => s.proId == id);
                
                if (resultQuery is null)
                {
                    throw new Exception($"Product has id {id} not found");
                } 
                
                var result = _mapper.Map<GetProductByIdDto>(resultQuery);

                var reviewQuery = await _shopContext.Reviews
                                    .Include(r => r.user)
                                    .Include(r => r.product)
                                    .Where(r => r.proId == id)
                                    .ToListAsync();

                result.reviews = reviewQuery
                                    .Select(r => _mapper.Map<GetReviewDto>(r))
                                    .ToList();
                                      

                if (result is not null)
                {
                    response.Data = result;
                }   

                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetProductByName(string name)
        {
            var response = new ServiceResponse<List<GetProductDto>>();
            try
            {
                var resultQuery = await _shopContext.Stocks
                                        .Include(s => s.category)
                                        .Include(s => s.product)
                                        .Where(s => s.product.productName.Contains(name))
                                        .ToListAsync();
                
                if (resultQuery is null)
                {
                    throw new Exception($"Product has name {name} not found");
                } 
                
                var result = resultQuery
                            .OrderBy(s => s.proId)
                            .Select(s => _mapper.Map<GetProductDto>(s))
                            .ToList();      

                if (result is not null)
                {
                    response.Data = result;
                }   

                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public Task<ServiceResponse<GetMediaDto>> UpdateMedia(UpdateMediaDto updateMedia, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updateProduct)
        {
            var response = new ServiceResponse<GetProductDto>();
            try
            { 
                var stock = await _shopContext.Stocks
                                .Include(s => s.category)
                                .Include(s => s.product)
                                .Where(s => s.vendorUserId == GetUserId() || GetUserRole() == "administrator")
                                .FirstOrDefaultAsync(s => s.proId == updateProduct.proId);
                
                var product = stock.product;
                
                if (product is null || stock is null)
                {
                    throw new Exception($"Product has id {updateProduct.proId} not found");
                }

                stock.cateId = (updateProduct.cateId != -1) ? updateProduct.cateId : stock.cateId;
                stock.proId = (updateProduct.proId != -1) ? updateProduct.proId : stock.proId;
                stock.priceUnit = (updateProduct.priceUnit != -1) ? updateProduct.priceUnit : stock.priceUnit;
                stock.quantity = (updateProduct.quantity != -1) ? updateProduct.quantity : stock.quantity;
                
                if (GetUserRole() != "administrator" && updateProduct.vendorUserId != -1)
                {
                    stock.vendorUserId = updateProduct.vendorUserId;
                }

                product.productName = (updateProduct.productName is not null) ? updateProduct.productName : product.productName;
                product.productDescription = (updateProduct.productDescription is not null) ? updateProduct.productDescription : product.productDescription;
                product.brand = (updateProduct.brand is not null) ? updateProduct.brand : product.brand;

                await _shopContext.SaveChangesAsync();

                var resultQuery = await _shopContext.Stocks
                                .Include(s => s.category)
                                .Include(s => s.product)
                                .FirstOrDefaultAsync(s => s.proId == updateProduct.proId);
                
                var result = _mapper.Map<GetProductDto>(resultQuery);                  

                if (result is not null)
                {
                    response.Data = result;
                }

                return response;           
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
        private int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext!.User
                .FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        private string GetUserRole()
        {
            return _httpContextAccessor.HttpContext!.User
                .FindFirstValue(ClaimTypes.Role)!;
        }
    }
}