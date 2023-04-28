using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Shop.Models
{
    public class Products
    {
        [Key]
        public int ProductId {get; set;}
        public string Name {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public string Status {get; set;} = string.Empty;
        public double Price {get; set;}
        public int Quantity {get; set;}
        public string Brand {get; set;} = string.Empty;
        public int Rating {get; set;}
        public int CategoryId {get; set;}
        public Category? Category {get; set;}
        public List<Review>? reviews {get; set;}
        public List<UserProduct>? product{get; set;}
    }
}