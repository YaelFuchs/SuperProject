using Microsoft.AspNetCore.Http;
using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Repositories
{
    public interface IProductRepositoy
    {
        public List<Product> GetProductList();
        public Product GetProductById(int Id);
        public void AddProduct(Product product);
        public void UpdateProduct(int Id, Product product);
        public void DeleteProduct(int Id);
        public List<Product> Search(string word);

    }
}