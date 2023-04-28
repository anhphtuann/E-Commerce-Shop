namespace API.Dtos.Carts
{
    public class GetMyCartDto
    {
        public decimal totalPrice { get; set; }
        public ICollection<GetMyCartProductDto> products { get; set; }
    }
}