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
    public class ProductRepository:IProductRepositoy
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public List<Product> GetProductList()
        {
            return _context.Products
           .Include(p => p.Category) // מביא גם את הקטגוריה
           .ToList();
        }

        public Product GetProductById(int Id)
        {
            var product = _context.Products
<<<<<<< Updated upstream
            .Include(p => p.Category) // מביא גם את הקטגוריה
            .FirstOrDefault(p => p.Id == Id);
=======
        .Include(p => p.Category) // מביא גם את הקטגוריה
        .FirstOrDefault(p => p.Id == Id);
>>>>>>> Stashed changes
            if (product != null)
            {
                return product;
            }
            return null;
        }
        public void AddProduct(Product product)
        {
<<<<<<< Updated upstream
=======
            Console.WriteLine($"id: {product.CategoryId}"); // הדפסה של CategoryId
>>>>>>> Stashed changes
            var existingProduct = _context.Products.FirstOrDefault(p => p.Name == product.Name);

            if (existingProduct == null)
            {
<<<<<<< Updated upstream
                var category = _context.Categories.FirstOrDefault(c => c.Id == product.CategoryId);

=======
              var category = _context.Categories.FirstOrDefault(c => c.Id == product.CategoryId);

                Console.WriteLine($"category: {category?.Name ?? "null"}"); // הדפסת שם הקטגוריה
                Console.WriteLine($"id: {category?.Id ?? 0}");

>>>>>>> Stashed changes
                if (category != null)
                {
                    product.Category = category; // קישור ל-Navigation Property

<<<<<<< Updated upstream
=======
                    Console.WriteLine($"cateroryyyyyy: {category}" );
>>>>>>> Stashed changes
                    _context.Products.Add(product); // הוספת המוצר ל-context
                    _context.SaveChanges(); // שמירת השינויים
                    return; // יציאה מהמתודה אם הצלחנו
                }
                else
                {
                    throw new Exception($"קטגוריה עם Id {product.CategoryId} לא נמצאה בדאטהבייס");
                }
            }
            else
            {
                throw new Exception("מוצר עם שם זה כבר קיים");
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
