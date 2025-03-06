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
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly DataContext _context;
        public ShoppingCartRepository(DataContext context)
        {
           _context = context;
        }

        public void addShoppingCart(int userId)
        {
            var cart = new ShoppingCart
            {
                UserId = userId,
            };
            _context.ShoppingCarts.Add(cart);
            _context.SaveChanges();
        }

        public void AddProduct(int userId, Product product, double quantity)
        {
            // בדיקה אם המוצר קיים במסד הנתונים
            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                throw new Exception("המוצר לא נמצא במסד הנתונים.");
            }

            var cart = _context.ShoppingCarts
                .Include(c => c.Carts) // חשוב כדי לטעון את הפריטים בסל
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId };
                _context.ShoppingCarts.Add(cart);
                _context.SaveChanges(); // שמירה כדי לקבל מזהה תקף לסל
            }

            var existingItem = cart.Carts.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Carts.Add(new ShoppingCartItem { ProductId = product.Id, Quantity = quantity });
            }

            _context.SaveChanges();
        }


        public void RemoveProduct(int userId,Product product)
        {
            var cart = _context.ShoppingCarts
                .Include(c => c.Carts)
                .FirstOrDefault(c => c.UserId == userId);
            var existingItem = cart.Carts.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingItem.Quantity == 0)
            {
                _context.ShoppingCartsItem.Remove(existingItem);
                cart.Carts.Remove(existingItem);
            }
            else
            {
                existingItem.Quantity -= 1;
            }

            _context.SaveChanges();
        }

        public void ClearCart(int userId)
        {
            var cart = _context.ShoppingCarts
                .Include(c => c.Carts) // טוען גם את הפריטים של הסל
                .FirstOrDefault(c => c.UserId == userId);

            if (!cart.Carts.Any())
                return; // אם הסל לא קיים או ריק - אין מה למחוק

            // מחיקת כל הפריטים מה-DbSet של ShoppingCartItems
            _context.ShoppingCartsItem.RemoveRange(cart.Carts);

            // ניקוי הרשימה מתוך האובייקט של הסל
            cart.Carts.Clear();

            _context.SaveChanges(); // שמירת השינויים בבסיס הנתונים
        }

        public List<ShoppingCartItem> GetShoppingCarts(int userId)
        {
            var cart =  _context.ShoppingCarts
                .Include(c => c.Carts) // טוען גם את הפריטים של הסל
                .ThenInclude(item => item.Product)
                .ThenInclude(c=> c.Category)
                .FirstOrDefault(c => c.UserId == userId);
            if(cart != null)
            {
                return cart.Carts;
            }
            else
            {
                return new List<ShoppingCartItem>();
            }
        }

    }
}
