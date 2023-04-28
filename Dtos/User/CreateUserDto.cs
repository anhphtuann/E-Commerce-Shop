using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Shop.Dtos.User
{
    public class CreateUserDto
    {
        public string UserName {get; set;} = string.Empty;
        public string Password {get; set;} = string.Empty;
        public string Role {get; set;} = string.Empty;
        public string Email{get; set;} = string.Empty;
        public string Contact {get; set;} = string.Empty;
    }
}