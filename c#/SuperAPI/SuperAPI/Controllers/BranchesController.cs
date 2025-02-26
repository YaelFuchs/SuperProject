using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Super.Core;
using Super.Core.DTOs;
using Super.Core.Models;
using Super.Core.Service;
using SuperAPI.Models;
using System.Collections.Generic;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ROLE_ADMIN")]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _IbranchService;
        private readonly IMapper _mapper;

        public BranchesController(IBranchService IbranchService, IMapper mapper)
        {
            _IbranchService = IbranchService;
            _mapper = mapper;
        }
        // GET: api/<BranchesController>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetAllBranches()
        {
            var branches = _mapper.Map<List<BranchDto>>(_IbranchService.GetAllBranches());
            return Ok(branches);
        }

        // GET api/<BranchesController>/5
        [AllowAnonymous]
        [HttpGet("{Id}")]
        public BranchDto GetBranchById(int Id)
        {
            var branch = _IbranchService.GetBranchById(Id);
            var branchDto=_mapper.Map<BranchDto>(branch);
            return branchDto ;
        }

        // POST api/<BranchesController>
        [HttpPost]
        public void AddBranch([FromBody] BranchPostModel branch)
        {
            
            _IbranchService.AddBranch(_mapper.Map<Branch>(branch));
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
