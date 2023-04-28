namespace API.Dtos.Carts
{
    public class GetMyCartProductDto
    {
        public string productName { get; set; }
        public long quantity { get; set; }
        public decimal priceUnit { get; set; }
        public decimal totalProductPrice { get; set; }
    }
}