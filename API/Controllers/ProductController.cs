
using API.Dtos.Media;
using API.Dtos.Products;
using API.Dtos.Reviews;
using API.Models.ServiceResponse;

namespace API.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("[controller]")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> GetAllProductByCategory(string category)
        {
            var response = await _productService.GetAllProductByCategory(category);
            return Ok(response);
        }

        [HttpPost("[controller]")]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> AddProduct(AddProductDto newProduct)
        {
            var response = await _productService.AddProduct(newProduct);
            return Ok(response);
        }

        [HttpGet("[controller]/{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductByIdDto>>> GetProductById(int id)
        {
            var response = await _productService.GetProductById(id);
            return Ok(response);
        }

        [HttpPatch("[controller]")]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> UpdateProduct(UpdateProductDto updateProduct)
        {
            var response = await _productService.UpdateProduct(updateProduct);
            return Ok(response);
        }

        [HttpPost("[controller]/{id}/reviews")]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> AddReview(AddReviewDto newReview)
        {
            var response = await _productService.AddReview(newReview);
            return Ok(response);
        }

        [HttpDelete("[controller]/{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProduct(id);
            return Ok(response);
        }

        [HttpPatch("media/{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> UpdateMedia(UpdateMediaDto updateMedia, int id)
        {
            var response = await _productService.UpdateMedia(updateMedia, id);
            return Ok(response);
        }

        [HttpPost("uploads")]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> AddMedia(AddMediaDto newMedia)
        {
            var response = await _productService.AddMedia(newMedia);
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> GetProductByName(string keyword)
        {
            var response = await _productService.GetProductByName(keyword);
            return Ok(response);
        }

        [HttpGet("get-all-categories")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> GetAllProduct()
        {
            var response = await _productService.GetAllProduct();
            return Ok(response);
        }

    }
}