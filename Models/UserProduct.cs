using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Shop.Models
{
    public class UserProduct
    {
        public int Id{get; set;}
        public int UserId {get; set;}
        public User? User {get; set;}
        public int ProductsId{get; set;}
        public Products? Products{get; set;}

    }
}