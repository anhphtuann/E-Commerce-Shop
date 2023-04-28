using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Users
{
    public class UserRegisterDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}