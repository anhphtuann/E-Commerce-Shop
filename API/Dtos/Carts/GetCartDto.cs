using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Carts
{
    public class GetCartDto
    {
        public int userId { get; set; }
        public decimal totalPrice { get; set; }
        public ICollection<GetCartProductDto> products { get; set; }
    }
}