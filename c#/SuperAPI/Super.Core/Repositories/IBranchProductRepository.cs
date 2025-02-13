using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Repositories
{
    public interface IBranchProductRepository
    {
        public List<BranchProduct> GetBranchProductList();
        public BranchProduct GetBranchProductById(int Id);
        public void AddBranchProduct(BranchProduct branchProduct);
        public void UpdateBranchProduct(int Id, BranchProduct branchProduct);
        public void DeleteBranchProduct(int Id);

    }
}
