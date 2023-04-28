using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Shop.Dtos.User
{
    public class BodyChangePasswordDto
    {
        public string oldPassword{get; set;} = string.Empty;
        public string newPassword {get; set;} = string.Empty;
    }
}