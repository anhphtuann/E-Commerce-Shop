using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Shop.Dtos.User
{
    public class BodyUpdateUserById
    {
        public string Contact {get; set;} = string.Empty;
        public string UserName {get; set;} = string.Empty;
    }
}