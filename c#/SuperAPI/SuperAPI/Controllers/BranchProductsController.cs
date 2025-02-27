using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Super.Core;
using Super.Core.Models;
using Super.Core.Service;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "User")]
    public class BranchProductsController : ControllerBase
    {
        private readonly IBranchProductService _IbranchProduct;

        public BranchProductsController(IBranchProductService IbranchProduct)
        {
            _IbranchProduct = IbranchProduct;
        }
        // GET: api/<BranchProductsController>
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<BranchProduct> GetAllBranchProducts()
        {
            return _IbranchProduct.GetAllBranchProducts();
        }

        // GET api/<BranchProductsController>/5
        [HttpGet("{Id}")]
        public BranchProduct GetBranchProductById(int Id)
        {
            return _IbranchProduct.GetBranchProductById(Id);
        }

        // POST api/<BranchProductsController>
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public void AddBranchProduct([FromBody] BranchProduct branchProduct)
        {
            _IbranchProduct.AddBranchProduct(branchProduct);
        }

        // PUT api/<BranchProductsController>/5
        [Authorize(Policy = "Admin")]
        [HttpPut("{Id}")]
        public void UpdateBranchProduct(int Id, [FromBody] BranchProduct branchProduct)
        {
            _IbranchProduct.UpdateBranchProduct(Id, branchProduct);
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
