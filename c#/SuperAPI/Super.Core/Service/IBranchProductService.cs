using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Service
{
    public interface IBranchProductService
    {
        public List<BranchProduct> GetAllBranchProducts();
        public BranchProduct GetBranchProductById(int id);
        public void AddBranchProduct(BranchProduct branchProduct);
        public void UpdateBranchProduct(int Id, BranchProduct branchProduct);
        public void DeleteBranchProduct(int Id);
    }
}
