using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Shop.Dtos.Reviews
{
    public class BodyPostReviewDto
    {
        public int Rate {get; set;}
        public string? Comment {get; set;}
        public string? Date{get; set;}
    }
}