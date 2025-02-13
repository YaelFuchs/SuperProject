using Super.Core.Models;
using Super.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Data.Repositories
{
    public class BranchProductRepository:IBranchProductRepository
    {
        private readonly DataContext _context;
        public BranchProductRepository(DataContext context)
        {
            _context = context;
        }
        public List<BranchProduct> GetBranchProductList()
        {
            return _context.BranchProducts.ToList();
        }
        public BranchProduct GetBranchProductById(int Id)
        {
            var branchProduct = _context.BranchProducts.Find(Id);
            if (branchProduct != null)
            {
                return branchProduct;
            }
            return null;
        }
        public void AddBranchProduct(BranchProduct branchProduct)
        {
          
                _context.BranchProducts.Add(branchProduct);
                _context.SaveChanges();
            

        }
        public void UpdateBranchProduct(int Id, BranchProduct branchProduct)
        {
            var branchProductToUpdate = _context.BranchProducts.Find(Id);
            if (branchProductToUpdate != null)
            {
                if (branchProductToUpdate.Price!=branchProduct.Price)
                {
                    branchProductToUpdate.Price = branchProductToUpdate.Price;

                }
                if (branchProductToUpdate.Product != branchProduct.Product)
                {
                    branchProductToUpdate.Product = branchProductToUpdate.Product;
                }
                if (branchProductToUpdate.Branch != branchProduct.Branch)
                {
                    branchProductToUpdate.Branch = branchProductToUpdate.Branch;
                }
                _context.SaveChanges();

            }

        }
        public void DeleteBranchProduct(int Id)
        {
            var branchProductToRemove = _context.BranchProducts.Find(Id);
            if (branchProductToRemove != null)
            {
                _context.BranchProducts.Remove(branchProductToRemove);
                _context.SaveChanges();
            }
        }
    }
}
