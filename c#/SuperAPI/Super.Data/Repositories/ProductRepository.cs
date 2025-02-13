using Super.Core.Models;
using Super.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Data.Repositories
{
    public class ProductRepository:IProductRepositoy
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public List<Product> GetProductList()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int Id)
        {
            var product = _context.Products.Find(Id);
            if (product != null)
            {
                return product;
            }
            return null;
        }
        public void AddProduct(Product product)
        {
            // בדיקה האם קיים משתמש עם אותו שם משתמש
            var existingProduct = _context.Products.FirstOrDefault(p => p.Name == p.Name);

            if (existingProduct == null) // אם לא קיים משתמש עם שם משתמש זהה
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }

        }
        public void UpdateProduct(int Id, Product product)
        {
            var productToUpdate = _context.Products.Find(Id);
            if (productToUpdate != null)
            {
                if (!productToUpdate.Name.Equals(product.Name))
                {
                    productToUpdate.Name = product.Name;

                }
                if (productToUpdate.Category != product.Category)
                {
                    productToUpdate.Category = product.Category;
                }
                if(productToUpdate.UnitOfMeasure!=product.UnitOfMeasure)
                {
                    productToUpdate.UnitOfMeasure = product.UnitOfMeasure;
                }
                _context.SaveChanges();

            }

        }
        public void DeleteProduct(int Id)
        {
            var productToRemove = _context.Products.Find(Id);
            if (productToRemove != null)
            {
                _context.Products.Remove(productToRemove);
                _context.SaveChanges();
            }
        }
    }
}
