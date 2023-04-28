using System.Security.Claims;
using API.Dtos.Carts;
using API.Models.Carts;
using API.Models.ServiceResponse;
using AutoMapper;

namespace API.Service.Carts
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly ShopContext _shopContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartService(IMapper mapper, ShopContext shopContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _shopContext = shopContext;
            _mapper = mapper;
            
        }
        public async Task<ServiceResponse<GetCartDto>> AddCart()
        {
            var response = new ServiceResponse<GetCartDto>();
            try
            {   
                var userIdNow = GetUserId();

                var isCartExist = await _shopContext.Carts
                                        .FirstOrDefaultAsync(c => c.userId == userIdNow);
                
                if (isCartExist is not null)
                {
                    throw new Exception("Cart existing");
                }

                var newCart = new Cart() {userId = userIdNow, totalPrice = 0};

                _shopContext.Carts.Add(newCart);
                await _shopContext.SaveChangesAsync();

                var cart = await _shopContext.Carts
                                        .OrderByDescending(c => c.cartId)
                                        .FirstOrDefaultAsync(c => c.userId == userIdNow);
                
                if (cart is null)
                {
                    throw new Exception("Create cart fail");
                }

                var result = _mapper.Map<GetCartDto>(cart);

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

        public async Task<ServiceResponse<GetCartDto>> AddItem(AddCartProductDto newProduct)
        {
            var response = new ServiceResponse<GetCartDto>();
            try
            {   
                var userIdNow = GetUserId();

                var cart = await _shopContext.Carts
                                        .FirstOrDefaultAsync(c => c.userId == userIdNow);
                
                if (cart is null)
                {
                    throw new Exception("Cart not existing, let's create cart");
                }

                bool isItemExist = cart.products
                                        .Any(p => p.proId == newProduct.proId);
                
                if (isItemExist)
                {
                    throw new Exception("Product existing");
                }

                var quantityStock = await _shopContext.Stocks
                                                    .Where(s => s.proId == newProduct.proId)
                                                    .Select(s => s.quantity)
                                                    .FirstOrDefaultAsync();

                if (newProduct.quantity > quantityStock)
                {
                    throw new Exception("Quantity request larger than stock");
                }

                var newCartProduct = _mapper.Map<CartProduct>(newProduct);

                cart.products.Add(newCartProduct);

                await _shopContext.SaveChangesAsync();

                var resultQuery = await _shopContext.Carts
                                        .Include(c => c.products)
                                        .FirstOrDefaultAsync(c => c.userId == userIdNow);

                if (resultQuery is null)
                {
                    throw new Exception("Wrong when query result");
                }

                var result = _mapper.Map<GetCartDto>(resultQuery);

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

        public Task<ServiceResponse<GetCartDto>> DeleteCartById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetCartDto>> DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetCartDto>> DeleteMyCart()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<GetCartDto>> GetCartById(int id)
        {
            var response = new ServiceResponse<GetCartDto>();
            try
            {
                var cart = await _shopContext.Carts
                                .Include(c => c.products)
                                .ThenInclude(p => p.product) 
                                .FirstOrDefaultAsync(c => c.userId == id);

                var result = _mapper.Map<GetCartDto>(cart);
                
                
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

        public async Task<ServiceResponse<GetMyCartDto>> GetMyCart()
        {
            var response = new ServiceResponse<GetMyCartDto>();
            try
            {
                var cart = await _shopContext.Carts
                                .Include(c => c.products)
                                .ThenInclude(p => p.product) 
                                .FirstOrDefaultAsync(c => c.userId == GetUserId());

                var result = _mapper.Map<GetMyCartDto>(cart);
                
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

        public Task<ServiceResponse<GetCartDto>> UpdateCart(int id, UpdateCartDto updateCart)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetCartDto>> UpdateItem(int id, UpdateCartProductDto updateProduct)
        {
            throw new NotImplementedException();
        }

        private int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext!.User
                .FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
    }
}