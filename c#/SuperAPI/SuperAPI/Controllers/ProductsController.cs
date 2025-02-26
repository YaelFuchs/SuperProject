using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Super.Core;
using Super.Core.Models;
using Super.Core.Service;
using Super.Service;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ROLE_ADMIN")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/<ProductsController>
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return _productService.GetAllProducts();
        }

        // GET api/<ProductsController>/5
        [AllowAnonymous]
        [HttpGet("{Id}")]
        public Product GetProductById(int Id)
        {
            return _productService.GetProductById(Id);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            _productService.AddProduct(product);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{Id}")]
        public void Put(int Id, [FromBody] Product product)
        {
            _productService.UpdateProduct(Id, product);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{Id}")]
        public void Delete(int Id)
        {
            _productService.DeleteProduct(Id);
        }
    }
}
