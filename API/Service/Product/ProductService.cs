using API.Dtos.Media;
using API.Dtos.Product;
using API.Dtos.Review;
using API.Models.ServiceResponse;
using AutoMapper;

namespace API.Service.Product
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

        public Task<ServiceResponse<GetProductDto>> AddProduct(AddProductDto newProduct)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetReviewDto>> AddReview(AddReviewDto newReview)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetProductDto>> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetProductDto>>> GetAllProduct()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetProductDto>>> GetAllProductByCategory(string categoryName)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<GetProductDto>> GetProductById(int id)
        {
            var response = new ServiceResponse<GetProductDto>();
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

        public Task<ServiceResponse<List<GetProductDto>>> GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetMediaDto>> UpdateMedia(UpdateMediaDto updateMedia, int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updateProduct)
        {
            throw new NotImplementedException();
        }
    }
}