using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Shop.Dtos.User
{
    public class UserProfile
    {
        public string Roles {get; set;} = string.Empty;
        public string Email {get; set;} = string.Empty;
        public string Username {get; set;} = string.Empty;
    }
}