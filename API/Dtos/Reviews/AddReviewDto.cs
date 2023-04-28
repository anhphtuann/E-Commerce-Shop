using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Reviews
{
    public class AddReviewDto
    {
        public double rate { get; set; }
        public string comment { get; set; }
    }
}