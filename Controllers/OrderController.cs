using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Shop.Dtos.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Shop.Controllers
{
    [ApiController]
    [Route("v1/orders")]
    [Authorize]
    public class OrderController
    {
        // [HttpPost]
        // public async Task CreatOrder([FromBody] BodyOrderDto body) {

        // }
    }
}