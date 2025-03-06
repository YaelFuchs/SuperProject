using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Repositories
{
    public interface IShoppingCartRepository
    {
        public void addShoppingCart(int userId);
        public void AddProduct(int userId, Product product, double quantity);
        public void RemoveProduct(int userId, Product product);
        public void ClearCart(int userId);
        public List<ShoppingCartItem> GetShoppingCarts(int userId);
    }
}
