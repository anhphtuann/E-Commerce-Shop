using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Shop.Dtos.User
{
    public class BodyChangeUserName
    {
        public string OldUserName{get; set;} = string.Empty;
        public string NewUserName{get; set;} = string.Empty;
    }
}