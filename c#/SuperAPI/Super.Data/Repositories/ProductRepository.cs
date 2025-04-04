﻿using Microsoft.AspNetCore.Http;
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
    public class ProductRepository : IProductRepositoy
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
            .Include(p => p.Category) // מביא גם את הקטגוריה
            .FirstOrDefault(p => p.Id == Id);


            if (product != null)
            {
                return product;
            }
            return null;
        }
        public void AddProduct(Product product)
        {
            var existingProduct = _context.Products.FirstOrDefault(p => p.Name == product.Name);

            if (existingProduct == null)
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == product.CategoryId);

                Console.WriteLine($"category: {category?.Name ?? "null"}"); // הדפסת שם הקטגוריה
                Console.WriteLine($"id: {category?.Id ?? 0}");

                if (category != null)
                {
                    product.Category = category; // קישור ל-Navigation Property


                    Console.WriteLine($"cateroryyyyyy: {category}");
                    _context.Products.Add(product); // הוספת המוצר ל-context
                    _context.SaveChanges();
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
            var productToUpdate = _context.Products.Find(product.Id);
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.CategoryId = product.CategoryId;
                productToUpdate.UnitOfMeasure = product.UnitOfMeasure;
                productToUpdate.ImageUrl = product.ImageUrl;

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception($"Database update error: {ex.InnerException?.Message ?? ex.Message}");
                }
            }
            else
            {
                throw new Exception("Product not found.");
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

        public Task<bool> ProcessPayment(string orderId, double sumForPay, string currency)
        {
            throw new NotImplementedException();
        }
        public List<Product> Search(string word)
        {
            return _context.Products.
            Where(p => p.Name.Contains(word))
            .Include(p => p.Category) // מביא גם את הקטגוריה
            .ToList();
        }
    }
}
