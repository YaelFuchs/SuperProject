using Microsoft.AspNetCore.Mvc;
using Super.Core;
using Super.Core.Models;
using Super.Core.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _IbranchService;

        public BranchesController(IBranchService IbranchService)
        {
            _IbranchService = IbranchService;
        }
        // GET: api/<BranchesController>
        [HttpGet]
        public IEnumerable<Branch> GetAllBranches()
        {
            return _IbranchService.GetAllBranches();
        }

        // GET api/<BranchesController>/5
        [HttpGet("{Id}")]
        public Branch GetBranchById(int Id)
        {
            return _IbranchService.GetBranchById(Id);
        }

        // POST api/<BranchesController>
        [HttpPost]
        public void AddBranch([FromBody] Branch branch)
        {
            _IbranchService.AddBranch(branch);
        }

        // PUT api/<BranchesController>/5
        [HttpPut("{Id}")]
        public void UpdateBranch(int Id, [FromBody] Branch branch)
        {
            _IbranchService.UpdateBranch(Id, branch);
        }

        // DELETE api/<BranchesController>/5
        [HttpDelete("{Id}")]
        public void DeleteBranch(int Id)
        {
            _IbranchService.DeleteBranch(Id);
        }
    }
}
