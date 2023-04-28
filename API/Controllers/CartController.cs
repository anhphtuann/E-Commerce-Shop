using API.Dtos.Carts;
using API.Models.ServiceResponse;
using API.Service.Carts;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
            
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCartDto>>> AddCart()
        {
            var response = await _cartService.AddCart();
            return Ok(response);
        }

        [HttpPost("create-item")]
        public async Task<ActionResult<ServiceResponse<GetCartDto>>> AddItem(AddCartProductDto newProduct)
        {
            var response = await _cartService.AddItem(newProduct);
            return Ok(response);
        }

        [HttpGet("my-carts")]
        public async Task<ActionResult<ServiceResponse<GetMyCartDto>>> GetMyCart()
        {
            var response = await _cartService.GetMyCart();
            return Ok(response);
        }

        [Authorize(Roles = "administrator")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCartDto>>> GetCartById(int id)
        {
            var response = await _cartService.GetCartById(id);
            return Ok(response);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCartDto>>> UpdateCart(int id, UpdateCartDto updateCart)
        {
            var response = await _cartService.UpdateCart(id, updateCart);
            return Ok(response);
        }

        [HttpPatch("manage-item/{id}")]
        public async Task<ActionResult<ServiceResponse<GetCartDto>>> UpdateCart(int id, UpdateCartProductDto updateProduct)
        {
            var response = await _cartService.UpdateItem(id, updateProduct);
            return Ok(response);
        }

        [HttpDelete("manage-item/{id}")]
        public async Task<ActionResult<ServiceResponse<GetCartDto>>> DeleteItem(int id)
        {
            var response = await _cartService.DeleteItem(id);
            return Ok(response);
        }

        [HttpDelete("manage-item/all")]
        public async Task<ActionResult<ServiceResponse<GetCartDto>>> DeleteMyCart()
        {
            var response = await _cartService.DeleteMyCart();
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCartDto>>> DeleteCartById(int id)
        {
            var response = await _cartService.DeleteCartById(id);
            return Ok(response);
        }
    }
}