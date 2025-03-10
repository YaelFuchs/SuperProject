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
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BranchProductsController : ControllerBase
    {
        private readonly IBranchProductService _IbranchProduct;
        private readonly IMapper _mapper;

        public BranchProductsController(IBranchProductService IbranchProduct, IMapper mapper)
        {
            _IbranchProduct = IbranchProduct;
            _mapper = mapper;
        }
        // GET: api/<BranchProductsController>
        [Authorize(Policy = "User")]
        [HttpGet]
        public ActionResult GetAllBranchProducts()
        {
            try
            {
                return Ok(_mapper.Map<List<BranchProductDto>>(_IbranchProduct.GetAllBranchProducts()));
            }
            catch (Exception ex)
            {
                return BadRequest("problen with fetch the Brabch Product List");
            }
        }

        // GET api/<BranchProductsController>/5
        [Authorize(Policy = "User")]
        [HttpGet("{Id}")]
        public BranchProductDto GetBranchProductById(int Id)
        {
            return _mapper.Map<BranchProductDto>(_IbranchProduct.GetBranchProductById(Id));
        }

        // POST api/<BranchProductsController>
        [HttpPost]
        public void AddBranchProduct([FromBody] BranchProductPostModel branchProduct)
        {
            try
            {
                _IbranchProduct.AddBranchProduct(_mapper.Map<BranchProduct>(branchProduct));
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
        }
        // PUT api/<BranchProductsController>/5
        [HttpPut("{Id}")]
        public void UpdateBranchProduct(int Id, [FromBody] BranchProductPostModel branchProduct)
        {
            try
            {
                _IbranchProduct.UpdateBranchProduct(Id, _mapper.Map<BranchProduct>(branchProduct));
            }
            catch (Exception ex)
            {
                BadRequest("There was a problem during the update.");
            }
        }
        // DELETE api/<BranchProductsController>/5
        [HttpDelete("{Id}")]
        public void DeleteBranchProduct(int Id)
        {

            _IbranchProduct.DeleteBranchProduct(Id);
        }
    }
}
