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
        public int ReId{get; set;}
        public int Rate {get; set;}
        public string Comment {get; set;} = string.Empty;
        public string Date {get; set;} = string.Empty;
        public int UserId {get; set;}
        public User? User {get; set;}
        public Products? Product {get; set;}
    }
}