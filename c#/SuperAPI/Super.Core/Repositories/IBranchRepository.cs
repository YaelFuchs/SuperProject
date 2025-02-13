using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Repositories
{
    public interface IBranchRepository
    {
        public List<Branch> GetBranchesList();
        public Branch GetBranchById(int Id);
        public void AddBranch(Branch branch);
        public void UpdateBranch(int Id, Branch branch);
        public void DeleteBranch(int Id);

    }
}
