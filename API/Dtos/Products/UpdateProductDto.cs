using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Products
{
    public class UpdateProductDto
    {
        public int proId { get; set; } = -1;
        public string productName { get; set; }
        public string productDescription { get; set; }
        public string brand { get; set; }
        public bool status { get; set; }
        public decimal priceUnit { get; set; } = -1;
        public long quantity { get; set; } = -1;
        public int cateId { get; set; } = -1;

    }
}