using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Shop.Models
{
    public class User
    {
        public int UserId {get; set;}
        public string Roles {get; set;} = string.Empty;
        public string Email {get; set;} = string.Empty;
        public string Username {get; set;} = string.Empty;
        public byte[] PasswordHash{get; set;} = new byte[0];
        public byte[] PasswordSalt{get; set;} = new byte[0];
        public string Contact {get; set;} = string.Empty;
        public string Avartar {get; set;} = string.Empty;
        public List<UserProduct>? order {get;set;}
    }
}