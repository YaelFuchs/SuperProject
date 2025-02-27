using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Super.Core;
using Super.Core.DTOs;
using Super.Core.Models;
using Super.Core.Service;
using Super.Service;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "User")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        // GET: api/<ProductsController>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetAllProducts()
        {
            return Ok(_mapper.Map<List<ProductDto>>(_productService.GetAllProducts()));
        }

        // GET api/<ProductsController>/5
        [HttpGet("{Id}")]
        public ActionResult GetProductById(int Id)
        {
            return Ok(_mapper.Map<ProductDto>(_productService.GetProductById(Id)));
        }

        // POST api/<ProductsController>
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            _productService.AddProduct(product);
        }

        // PUT api/<ProductsController>/5
        [Authorize(Policy = "Admin")]
        [HttpPut("{Id}")]
        public void Put(int Id, [FromBody] Product product)
        {
            _productService.UpdateProduct(Id, product);
        }

        // DELETE api/<ProductsController>/5
        [Authorize(Policy = "Admin")]
        [HttpDelete("{Id}")]
        public void Delete(int Id)
        {
            _productService.DeleteProduct(Id);
        }
    }
}
