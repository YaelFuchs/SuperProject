using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Super.Core.DTOs;
using Super.Core.Models;
using Super.Core.Service;
using SuperAPI.Mapping;
using SuperAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public ShoppingCartController(IShoppingCartService shoppingCartService, IMapper mapper, IProductService productService)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
            _productService = productService;
        
        }
     

        // GET api/<ShoppingCartController>/5
        [HttpGet("{id}")]
        public ActionResult GetShoppingCarts(int userId)
        {
            return Ok(_mapper.Map<List<ShoppingCartItemDto>>(_shoppingCartService.GetShoppingCarts(userId)));
        }

        // POST api/<ShoppingCartController>
        [HttpPost]
        public void addShoppingCart(int userId)
        {
            _shoppingCartService.addShoppingCart(userId);
        }
        // POST api/<ShoppingCartController>
        [HttpPost("/addToCart")]
        public void AddProduct(int userId, [FromBody] ShoppingCartModel shoppingCart)
        {
            Console.WriteLine("what he give me: ", shoppingCart.ToString());

            // שליפת המוצר לפי השם
            var product = _productService.GetAllProducts()
                .FirstOrDefault(p => p.Name == shoppingCart.Product.Name);

            // אם המוצר לא נמצא, מחזירים שגיאה או מבצעים פעולה אחרת
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            // הוספת המוצר לסל
            _shoppingCartService.AddProduct(userId, product, shoppingCart.Quantity);
        }

        // PUT api/<ShoppingCartController>/5
        [HttpPut("{id}")]
        public void RemoveProduct(int userId, [FromBody] Product product)
        {
            _shoppingCartService.RemoveProduct(userId, product);
        }

        // DELETE api/<ShoppingCartController>/5
        [HttpDelete("{id}")]
        public void ClearCart(int userId)
        {
            _shoppingCartService.ClearCart(userId);
        }
    }
}
