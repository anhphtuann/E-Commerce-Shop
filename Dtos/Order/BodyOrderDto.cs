using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Shop.Dtos.Order
{
    public class BodyOrderDto
    {
        public string PaymentMethod {get; set;} = string.Empty;
        public string Address {get; set;} = string.Empty;
    }
}