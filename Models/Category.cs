using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Shop.Models
{
    public class Category
    {
        [Key]
        public int Id {get; set;}
        public string CateName {get; set;} = string.Empty;
        public string Location {get; set;} = string.Empty;
        public List<Products>? Products {get; set;}
    
    }
}