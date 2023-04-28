using API.Dtos.Carts;
using API.Models.ServiceResponse;

namespace API.Service.Carts
{
    public interface ICartService
    {
        Task<ServiceResponse<GetCartDto>> AddCart();
        Task<ServiceResponse<GetCartDto>> AddItem(AddCartProductDto newProduct);
        Task<ServiceResponse<GetMyCartDto>> GetMyCart();
        Task<ServiceResponse<GetCartDto>> GetCartById(int id);
        Task<ServiceResponse<GetCartDto>> UpdateCart(int id, UpdateCartDto updateCart);
        Task<ServiceResponse<GetCartDto>> UpdateItem(int id, UpdateCartProductDto updateProduct);
        Task<ServiceResponse<GetCartDto>> DeleteItem(int id);
        Task<ServiceResponse<GetCartDto>> DeleteMyCart();
        Task<ServiceResponse<GetCartDto>> DeleteCartById(int id); 
    }
}