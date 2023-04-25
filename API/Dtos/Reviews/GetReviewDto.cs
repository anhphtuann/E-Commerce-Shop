using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Reviews
{
    public class GetReviewDto
    {
        public string userName { get; set; }
        public double rate { get; set; }
        public string comment { get; set; }
        public string date { get; set; }
    }
}