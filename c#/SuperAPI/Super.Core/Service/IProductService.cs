using Microsoft.AspNetCore.Http;
using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Service
{
    public interface IProductService
    {
        public List<Product> GetAllProducts();
        public Product GetProductById(int id);
        public void AddProduct(Product product);
        public void UpdateProduct(int Id, Product product);
        public void DeleteProduct(int Id);
    }
}
