using Microsoft.EntityFrameworkCore;
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
            return _context.BranchProducts
            .Include(b => b.Branch)
            .Include(b => b.Product)
            .Include(b=>b.Product.Category)
           .ToList(); ;
        }
        public BranchProduct GetBranchProductById(int Id)
        {
            var branchProduct = _context.BranchProducts.Include(b => b.Branch)
            .Include(b => b.Product).Include(b => b.Product.Category).FirstOrDefault(b=>b.Id==Id);
            if (branchProduct != null)
            {
                return branchProduct;
            }
            return null;
        }
        public void AddBranchProduct(BranchProduct branchProduct)
        {
            var existingBranchProduct = _context.BranchProducts.FirstOrDefault(b => b.BranchId == branchProduct.BranchId && b.ProductId == branchProduct.ProductId);
            if (existingBranchProduct == null)
            {
              var branch= _context.Branches.FirstOrDefault(b=>b.Id==branchProduct.BranchId);
              var product= _context.Products.FirstOrDefault(p=>p.Id==branchProduct.ProductId);
            if (product == null || branch==null) {
                    throw new Exception("לא קיים ");
             }
            branchProduct.Product = product;
            branchProduct.Product.Category= product.Category;
            branchProduct.Branch = branch;
                _context.BranchProducts.Add(branchProduct);
                _context.SaveChanges();
            }
            else { 
            throw new Exception("המוצר קיים כבר בסניף");
            }


        }
        public void UpdateBranchProduct(int Id, BranchProduct branchProduct)
        {

            var branchProductToUpdate = _context.BranchProducts.Find(Id);
            if (branchProductToUpdate != null)
            {

                if (branchProductToUpdate.Price!=branchProduct.Price)
                {
                    branchProductToUpdate.Price = branchProduct.Price;

                }
                if (branchProductToUpdate.Product != branchProduct.Product)
                {
                    branchProductToUpdate.Product = branchProduct.Product;
                }
                if (branchProductToUpdate.Branch != branchProduct.Branch )
                {
                    branchProductToUpdate.Branch = branchProduct.Branch;
                    branchProductToUpdate.BranchId = branchProduct.Branch.Id;
                }
                if (branchProductToUpdate.BranchId != branchProduct.BranchId)
                {
                    branchProductToUpdate.BranchId = branchProduct.BranchId;
                    var branch = _context.Branches.Find(branchProduct.BranchId);
                    if (branch != null)
                    {
                        branchProductToUpdate.Branch = branch;
                    }

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
