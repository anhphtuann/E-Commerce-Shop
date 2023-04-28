using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Users
{
    public class UserLoginDto
    {
        public string account { get; set; }
        public string password { get; set; }
    }
}