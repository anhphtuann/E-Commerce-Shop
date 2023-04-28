namespace API.Dtos.Carts
{
    public class AddCartDto
    {
        public int userId { get; set; }
        public decimal totalPrice { get; set; }
    }
}