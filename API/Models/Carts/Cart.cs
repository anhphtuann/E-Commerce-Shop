using API.Models.Users;

namespace API.Models.Carts
{
    public class Cart
    {
        [Key]
        public int cartId { get; set; }
        public int userId { get; set; }

        [Column(TypeName = "money")]
        public decimal totalPrice { get; set; }
        public ICollection<CartProduct> products { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }
    }
}