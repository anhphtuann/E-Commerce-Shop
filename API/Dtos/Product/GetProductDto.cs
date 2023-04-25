using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Product
{
    public class GetProductDto
    {
        public int proId { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public string brand { get; set; }
        public bool status { get; set; } = false;
        public decimal priceUnit { get; set; }
        public long quantity { get; set; }
        public int cateId { get; set; }
        public string categoryName { get; set; }
        public string location { get; set; }
    }
}