using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Service
{
    public interface IBranchService
    {
        public List<Branch> GetAllBranches();
        public Branch GetBranchById(int id);
        public void AddBranch(Branch branch);
        public void UpdateBranch(int Id, Branch branch);
        public void DeleteBranch(int Id);
    }
}
