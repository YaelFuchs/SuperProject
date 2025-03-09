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
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }
        public void addShoppingCart(int userId)
        {
            _shoppingCartRepository.addShoppingCart(userId);
        }

        public void AddProduct(int userId, Product product)
        {
            _shoppingCartRepository.AddProduct(userId, product);
        }
        public void RemoveProduct(int userId, Product product)
        {
            _shoppingCartRepository.RemoveProduct(userId, product);
        }
        public void ClearCart(int userId)
        {
            _shoppingCartRepository.ClearCart(userId);
        }
        public List<ShoppingCartItem> GetShoppingCarts(int userId)
        {
            return _shoppingCartRepository.GetShoppingCarts(userId);
        }
    }
}
