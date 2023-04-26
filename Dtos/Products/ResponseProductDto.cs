using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Shop.Dtos.Products
{
    public class ResponseProductDto
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public string Status {get; set;} = string.Empty;
        public double Price {get; set;}
        public int Quantity {get; set;}
        public string Brand {get; set;} = string.Empty;
        public int Rating {get; set;}
        
    }
}