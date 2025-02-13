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
    public class ProductService: IProductService
    {
        
            private readonly IProductRepositoy _productRepository;
            public ProductService(IProductRepositoy productRepository)
            {
            _productRepository = productRepository;
            }
            public List<Product> GetAllProducts()
            {
                return _productRepository.GetProductList();
            }
            public Product GetProductById(int Id)
            {
                return _productRepository.GetProductById(Id);

            }
            public void AddProduct(Product product)
            {
                _productRepository.AddProduct(product);
            }
            public void UpdateProduct(int Id, Product product)
            {
                _productRepository.UpdateProduct(Id, product);
            }

            public void DeleteProduct(int Id)
            {
                _productRepository.DeleteProduct(Id);
            }

    }
}
