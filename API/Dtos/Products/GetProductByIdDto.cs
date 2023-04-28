using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Reviews;
using API.Models.Reviews;

namespace API.Dtos.Products
{
    public class GetProductByIdDto
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
        public int vendorUserId { get; set; }
        public DateTime dateModify { get; set; }
        public ICollection<GetReviewDto> reviews { get; set; }
    }
}