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
    public class BranchProductService : IBranchProductService
    {
        private readonly IBranchProductRepository _branchProductRepository;
        public BranchProductService(IBranchProductRepository branchProductRepository)
        {
            _branchProductRepository = branchProductRepository;
        }
        public List<BranchProduct> GetAllBranchProducts()
        {
            return _branchProductRepository.GetBranchProductList();
        }
        public BranchProduct GetBranchProductById(int Id)
        {
            return _branchProductRepository.GetBranchProductById(Id);

        }
        public void AddBranchProduct(BranchProduct branchProduct)
        {
            _branchProductRepository.AddBranchProduct(branchProduct);
        }
        public void UpdateBranchProduct(int Id, BranchProduct branchProduct)
        {
            _branchProductRepository.UpdateBranchProduct(Id, branchProduct);
        }

        public void DeleteBranchProduct(int Id)
        {
            _branchProductRepository.DeleteBranchProduct(Id);
        }
    }
}
