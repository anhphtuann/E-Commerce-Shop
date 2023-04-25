using API.Dtos.Media;
using API.Dtos.Products;
using API.Dtos.Reviews;
using API.Models.Products;
using API.Models.ServiceResponse;
using AutoMapper;

namespace API.Service.Products
{
    public class ProductService : IProductService
    {
        private readonly ShopContext _shopContext;
        private readonly IMapper _mapper;
        public ProductService(IMapper mapper, ShopContext shopContext)
        {
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

        public Task<ServiceResponse<GetReviewDto>> AddReview(AddReviewDto newReview)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetProductDto>> DeleteProduct(int id)
        {
            throw new NotImplementedException();
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
                                .FirstOrDefaultAsync(s => s.proId == updateProduct.proId);
                
                var product = stock.product;
                
                if (product is null || stock is null)
                {
                    throw new Exception($"Product has id {updateProduct.proId} not found");
                }

                stock.cateId = updateProduct.cateId != -1 ? updateProduct.cateId : stock.cateId;
                stock.proId = updateProduct.proId != -1 ? updateProduct.proId : stock.proId;
                stock.priceUnit = updateProduct.priceUnit != -1 ? updateProduct.priceUnit : stock.priceUnit;
                stock.quantity = updateProduct.quantity != -1 ? updateProduct.quantity : stock.quantity;

                product.productName = updateProduct.productName is not null ? updateProduct.productName : product.productName;
                product.productDescription = updateProduct.productDescription is not null ? updateProduct.productDescription : product.productDescription;
                product.brand = updateProduct.brand is not null ? updateProduct.brand : product.brand;

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
    }
}