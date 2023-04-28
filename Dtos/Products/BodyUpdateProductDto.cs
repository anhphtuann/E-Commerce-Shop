using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Shop.Dtos.Products
{
    public class BodyUpdateProductDto
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string Brand {get; set;} = string.Empty;
        public string CategoryProduct {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public string Status {get; set;} = string.Empty;
        public int Price {get; set;}
        public int Rating{get; set;}
        public int CountInStock{get; set;}
        public int Quantity{get; set;}
    }
}