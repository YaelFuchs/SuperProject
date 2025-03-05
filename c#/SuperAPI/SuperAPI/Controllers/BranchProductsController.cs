using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Super.Core;
using Super.Core.DTOs;
using Super.Core.Models;
using Super.Core.Service;
using SuperAPI.Models;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "User")]
    public class BranchProductsController : ControllerBase
    {
        private readonly IBranchProductService _IbranchProduct;
        private readonly IMapper _mapper;

        public BranchProductsController(IBranchProductService IbranchProduct ,IMapper mapper)
        {
            _IbranchProduct = IbranchProduct;
            _mapper = mapper;
        }
        // GET: api/<BranchProductsController>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetAllBranchProducts()
        {
            return Ok(_mapper.Map<List<BranchProductDto>>(_IbranchProduct.GetAllBranchProducts()));
        }

        // GET api/<BranchProductsController>/5
        [HttpGet("{Id}")]
        public BranchProductDto GetBranchProductById(int Id)
        {
            return _mapper.Map<BranchProductDto>(_IbranchProduct.GetBranchProductById(Id));
        }

        // POST api/<BranchProductsController>
        //[Authorize(Policy = "Admin")]
        [HttpPost]
        public void AddBranchProduct([FromBody] BranchProductPostModel branchProduct)
        {
            _IbranchProduct.AddBranchProduct(_mapper.Map<BranchProduct>(branchProduct));
        }

        // PUT api/<BranchProductsController>/5
        //[Authorize(Policy = "Admin")]
        [HttpPut("{Id}")]
        public void UpdateBranchProduct(int Id, [FromBody] BranchProductPostModel branchProduct)
        {
            Console.WriteLine("im here");
            _IbranchProduct.UpdateBranchProduct(Id, _mapper.Map<BranchProduct>(branchProduct));
        }

        // DELETE api/<BranchProductsController>/5
        [Authorize(Policy = "Admin")]
        [HttpDelete("{Id}")]
        public void DeleteBranchProduct(int Id)
        {
            _IbranchProduct.DeleteBranchProduct(Id);
        }
    }
}
