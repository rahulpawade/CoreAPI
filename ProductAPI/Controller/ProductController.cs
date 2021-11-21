using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProductAPI.Service;
using ProductAPI.Model;
using Microsoft.AspNetCore.Http;

namespace ProductAPI.Controller
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductController : ControllerBase
    {
        public readonly ILogger<ProductController> _logger;
        public readonly IProductRepo _productRepo;

        public ProductController(ILogger<ProductController> logger, IProductRepo productRepo)
        {
            _logger = logger;
            _productRepo = productRepo;
        }

        // GET api/Product
        [HttpGet]
        public IActionResult GetProductList()
        {
            var productList = _productRepo.GetProductList();
            if (productList == null)
                return NoContent();

            return Ok(productList);
        }

        // GET api/product/{id}
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productRepo.GetProductById(id);
            if (product == null)
                return NotFound("Sorry there is no product for given ID");

            return Ok(product);
        }

        // POST api/product
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product prod)
        {
            if (!TryValidateModel(prod))
            {
                return ValidationProblem(ModelState);
            }

            if (!_productRepo.AddProduct(prod))
                return StatusCode(StatusCodes.Status500InternalServerError, "Not able to add the product, Please check Logs for more Information");

            return Ok("Product added Successfully");
        }

        // PUT api/product/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            var detail = _productRepo.GetProductById(id);
            if (detail == null)
            {
                return NotFound("Sorry there is no product for given ID");
            }

            if (!TryValidateModel(product))
            {
                return ValidationProblem(ModelState);
            }

            detail.Name = !string.IsNullOrEmpty(product.Name) ? product.Name : detail.Name;
            detail.Description = !string.IsNullOrEmpty(product.Description) ? product.Description : detail.Description;
            detail.Price = !string.IsNullOrEmpty(product.Description) ? product.Price : detail.Price;

            if (!_productRepo.UpdateProduct(detail))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Not able to Update the product, Please check Logs for more Information");
            }

            return Ok("Updated Successfully");
        }

        // DELETE api/product/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var detail = _productRepo.GetProductById(id);
            if (detail == null)
            {
                return NotFound("Sorry there is no product for given ID");
            }

            if (!_productRepo.DeleteProduct(detail))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Not able to Delete the product, Please check Logs for more Information");
            }

            return Ok("Deleted Successfully");
        }

    }
}
