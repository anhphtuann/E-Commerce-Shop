namespace API.Dtos.Products
{
    public class GetProductByCategoryDto
    {
        public int proId { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public string brand { get; set; }
        public bool status { get; set; } = false;
        public decimal priceUnit { get; set; }
        public long quantity { get; set; }
    }
}