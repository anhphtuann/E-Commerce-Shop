using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Shop.Dtos.Category;
using E_Commerce_Shop.Dtos.Reviews;
using E_Commerce_Shop.Models;
using E_Commerce_Shop.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Shop.Controllers
{
    //property [FromRoute] [FromBody]
    [ApiController]
    [Route("v1/products")]
    public class ProductController: ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Authorize(Roles = "Customer,Admin")]
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ResponseProductDto>>>>GetAllProductByCategory(string category) {
            var response = await _productService.GetAll(category);
            if(response.Data is null) {
                return NotFound(response);
            }
            return Ok(response);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductBodyPostDto data){
            var check = await _productService.CreateProduct(data);
            if(!check) {
                return BadRequest(new {message = "Add Product False", status = 400});
            }
            return Ok(new {message = "Success add Product", status = 200});
        }
        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ResponseProductDto>>> GetProductById(int id) {
            var response = await _productService.GetProductById(id);
            if(response.Data is null) {
                return NotFound(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateProductById(int id, [FromBody] BodyUpdateProductDto body){
            var response = await _productService.UpdateProductById(id, body);
            if(response.Data is false) {
                return NotFound(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Customer, Admin")]
        [HttpPost("{id}/reviews")]
        public async Task<ActionResult<ServiceResponse<Review>>> CreateReviewForProduct(int id, [FromBody] BodyPostReviewDto body) {
            var response = await _productService.CreateReviewForProduct(id, body);
            if(response.Data is null) {
                return NotFound(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductById(int id) {
            var result = await _productService.DeleteProducById(id);
            if(!result) {
                return NotFound(new {message = "product not exist", status = 400});
            }
            return Ok(new {message = "Success delete product", status = 200});
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("search")]
        public async Task<ActionResult<ServiceResponse<List<ResponseProductDto>>>> SearchProducts(string keyword) {
            var result = await _productService.SearchProductByKeyWord(keyword);
            if(result.Data is null){
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpGet("get-all-categories")]
        public async Task<ActionResult<ServiceResponse<List<ResponseCategory>>>> GetAllCategories() {
            var result = await _productService.GetAllCategories();
            if(result.Data is null){
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}