using Microsoft.EntityFrameworkCore;
using Super.Core.DTOs;
using Super.Core.Models;
using Super.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void AddProduct(int userId, Product product)
        {
            // בדיקה אם המוצר קיים במסד הנתונים
            var existingProduct = _context.Products
                .FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                throw new Exception("המוצר לא נמצא במסד הנתונים.");
            }

            var cart = _context.ShoppingCarts
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.Id)  
                .Include(c => c.Carts) // חשוב כדי לטעון את הפריטים בסל
                .FirstOrDefault();

            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId };
                _context.ShoppingCarts.Add(cart);
                _context.SaveChanges(); // שמירה כדי לקבל מזהה תקף לסל
            }

            var existingItem = cart.Carts.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                cart.Carts.Add(new ShoppingCartItem { ProductId = product.Id, Quantity = 1 });
            }

            _context.SaveChanges();
        }


        public void RemoveProduct(int userId, Product product)
        {
            var cart = _context.ShoppingCarts
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.Id)
                .Include(c => c.Carts)
                .FirstOrDefault();

            if (cart == null)
                return;

            var existingItem = cart.Carts.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingItem == null)
                return;

            if (existingItem.Quantity == 1)
            {
                _context.ShoppingCartsItem.Remove(existingItem); // מחיקה מה-DB
                cart.Carts.Remove(existingItem); // מחיקה מהאוסף בזיכרון
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
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.Id)
                .Include(c => c.Carts) // טוען את הפריטים של הסל
                .FirstOrDefault();

            if (cart == null)
            {
                Console.WriteLine("⚠️ לא נמצא סל למשתמש עם ID: " + userId);
                return;
            }

            Console.WriteLine("✅ נמצא סל למשתמש: " + userId);
            Console.WriteLine("🔹 מספר פריטים בסל: " + cart.Carts.Count);

            // מחיקת כל הפריטים מה-DbSet של ShoppingCartItems
            _context.ShoppingCartsItem.RemoveRange(cart.Carts);
            Console.WriteLine("🗑️ פריטי הסל נמחקו");

            // מחיקת הסל עצמו מה-DbSet
            _context.ShoppingCarts.Remove(cart);
            Console.WriteLine("🗑️ הסל נמחק");

            _context.SaveChanges(); // שמירת השינויים בבסיס הנתונים
            Console.WriteLine("✅ שינויים נשמרו בהצלחה!");
        }


        public List<ShoppingCartItem> GetShoppingCarts(int userId)
        {
            var cart = _context.ShoppingCarts
             .Where(c => c.UserId == userId)
             .OrderByDescending(c => c.Id) // מזהה אחרון = סל אחרון
             .Include(c => c.Carts)
             .ThenInclude(item => item.Product)
             .ThenInclude(c => c.Category)
             .FirstOrDefault();
            if (cart != null)
            {
                return cart.Carts;
            }
            else
            {
                return new List<ShoppingCartItem>();
            }
        }
        public ResultDto CalculateCheapestCart(int userId)
        {
            var cart = _context.ShoppingCarts
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.Id)
                .Include(c => c.Carts)
                .ThenInclude(item => item.Product)
                .FirstOrDefault();

            if (cart == null || !cart.Carts.Any())
            {
                return null; // סל ריק או לא קיים
            }

            var products = cart.Carts
                .SelectMany(item => Enumerable.Repeat(item.Product, (int)item.Quantity))
                .ToList(); var branches = _context.Branches.ToList();

            int numBranches = branches.Count;
            int numProducts = products.Count;

            double[][] prices = new double[numBranches][];
            int[] shippingCosts = new int[numBranches];
            List<CartTotalPrice> cartTotalPriceList = new List<CartTotalPrice>();
            double cartCost = 0;

            for (int i = 0; i < numBranches; i++)
            {
                prices[i] = new double[numProducts];
                shippingCosts[i] = branches[i].ShippingCost;
               
                for (int j = 0; j < numProducts; j++)
                {
                    var productPrice = _context.BranchProducts
                        .FirstOrDefault(pp => pp.ProductId == products[j].Id && pp.BranchId == branches[i].Id);
                    prices[i][j] = productPrice?.Price ?? double.MaxValue;
                    cartCost+= productPrice.Price;
                }
                cartTotalPriceList.Add(new CartTotalPrice
                {
                    Id = i,
                    TotalCartCost = shippingCosts[i]+cartCost
                });
                cartCost = 0;
            }

            double bestCost = double.MaxValue;
            cartTotalPriceList = cartTotalPriceList.OrderBy(s => s.TotalCartCost).ToList();
            List<CartTotalPrice> top5Btanches = cartTotalPriceList.Take(5).ToList();
            HashSet<int> bestCombination = new HashSet<int>();

            // 1. בחירת הסניף הזול ביותר
            int cheapestBranchId = top5Btanches[0].Id;

            // 2. לולאה על שאר 4 הסניפים
            for (int subset = 1; subset < (1 << 4); subset++) // 1 << 4 = 16 (כל השילובים האפשריים של 4 סניפים)
            {
                HashSet<int> selectedBranches = new HashSet<int> { cheapestBranchId }; // תמיד כולל את הסניף הזול ביותר
                double shipping = shippingCosts[cheapestBranchId]; // עלות משלוח של הסניף הזול ביותר

                // 3. הוספת שאר הסניפים הנבחרים
                for (int i = 1; i < 5; i++)
                {
                    if ((subset & (1 << i)) != 0)
                    {
                        selectedBranches.Add(top5Btanches[i].Id); 
                        shipping += shippingCosts[top5Btanches[i].Id];
                    }
                }


                double[] cartPrices = CalculateCombinedCost(selectedBranches, prices, numProducts);
                double totalCost = cartPrices.Sum() + shipping;

                if (totalCost < bestCost)
                {
                    bestCost = totalCost;
                    bestCombination = selectedBranches;
                }
            }

            // יצירת תוצאה למשתמש
            // יצירת תוצאה למשתמש באמצעות DTO
            var userResult = products.Select((p, i) => new ProductPriceDto
            {
                Product = p,
                Price = CalculateCombinedCost(bestCombination, prices, numProducts)[i]
            }).ToList();

            // יצירת תוצאה למנהל
            var managerResult = new CheapestShoppingCartResult
            {
                BestCost = bestCost,
                SelectedBranch = bestCombination.Select(i => branches[i]).ToList(),
                ProductOrigins = new Dictionary<int, int>()
            };

            double[] bestCartPrices = CalculateCombinedCost(bestCombination, prices, numProducts);
            for (int i = 0; i < numProducts; i++)
            {
                int cheapestStore = -1;
                foreach (int superId in bestCombination)
                {
                    if (prices[superId][i] == bestCartPrices[i])
                    {
                        cheapestStore = superId;
                        break;
                    }
                }
                managerResult.ProductOrigins[products[i].Id] = branches[cheapestStore].Id;
            }
            var result = new ResultDto {Prices= userResult,
                                        CheapestShoppingCartResult = managerResult 
                                         };
            return result;
        }

        private double[] CalculateCombinedCost(HashSet<int> selectedBranches, double[][] prices, int numProducts)
        {
            double[] selectedProducts = new double[numProducts];
            Array.Fill(selectedProducts, double.MaxValue);

            foreach (int superId in selectedBranches)
            {
                for (int p = 0; p < numProducts; p++)
                {
                    selectedProducts[p] = Math.Min(selectedProducts[p], prices[superId][p]);
                }
            }
            return selectedProducts;
        }
    }
}

