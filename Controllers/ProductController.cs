using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Shop.Models;
using E_Commerce_Shop.Services.ProductService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Shop.Controllers
{
    [ApiController]
    [Route("v1/products")]
    public class ProductController: ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ResponseProductDto>>>>GetAllProductByCategory(string category) {
            var response = await _productService.GetAll(category);
            if(response.Data is null) {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductBodyPostDto data){
            var check = await _productService.CreateProduct(data);
            if(!check) {
                return BadRequest(new {message = "Add Product False", status = 400});
            }
            return Ok(new {message = "Success add Product", status = 200});
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ResponseProductDto>>> GetProductById(int id) {
            var response = await _productService.GetProductById(id);
            if(response.Data is null) {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}