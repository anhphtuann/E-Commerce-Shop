using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Shop.Models
{
    public class Review
    {
        [Key]
        public int Id{get; set;}
        public int Rate {get; set;}
        public string Comment {get; set;} = string.Empty;
        public string Date {get; set;} = string.Empty;
        public int ProductForeignId {get; set;}
        public Products? ProductForeign {get; set;}
    }
}