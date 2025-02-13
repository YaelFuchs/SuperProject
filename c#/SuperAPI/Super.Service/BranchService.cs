using Super.Core.Models;
using Super.Core.Repositories;
using Super.Core.Service;
using Super.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Service
{
    public class BranchService:IBranchService
    { 
          private readonly IBranchRepository _branchRepository;
    public BranchService(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }
    public List<Branch> GetAllBranches()
    {
        return _branchRepository.GetBranchesList();
    }
        public Branch GetBranchById(int Id)
        {
            return _branchRepository.GetBranchById(Id);

        }
        public void AddBranch(Branch branch)
        {
            _branchRepository.AddBranch(branch);
        }
        public void UpdateBranch(int Id, Branch branch)
        {
            _branchRepository.UpdateBranch(Id, branch);
        }

        public void DeleteBranch(int Id)
        {
            _branchRepository.DeleteBranch(Id);
        }

    }
}
