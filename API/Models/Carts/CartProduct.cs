using API.Models.Products;

namespace API.Models.Carts
{
    public class CartProduct
    {
        public int cartId { get; set; }
        public int proId { get; set; }

        [Column(TypeName = "money")]
        public decimal priceUnit { get; set; }
        public long quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal totalProductPrice { get; set; }

        [ForeignKey("cartId")]
        public Cart cart { get; set; }

        [ForeignKey("proId")]
        public Product product { get; set; }
    }
}