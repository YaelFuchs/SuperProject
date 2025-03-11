using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Super.Core.DTOs;
using Super.Core.Models;
using Super.Core.Service;
using SuperAPI.Mapping;
using SuperAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperAPI.Controllers
{
    [Authorize(Policy = "User")]
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
        [HttpGet("{userId}")]
        public ActionResult GetShoppingCarts(int userId)
        {
            try
            {
                var shoppingCartDtos = _mapper.Map<List<ShoppingCartItemDto>>(_shoppingCartService.GetShoppingCarts(userId));
                foreach (var cartDto in shoppingCartDtos)
                {
                    if (cartDto.Product.ImageUrl != null)
                    {
                        var path = Path.Combine(Environment.CurrentDirectory, "images/", cartDto.Product.ImageUrl);
                        if (System.IO.File.Exists(path))
                        {
                            byte[] bytes = System.IO.File.ReadAllBytes(path);
                            string imageBase64 = Convert.ToBase64String(bytes);
                            cartDto.Product.ImageUrl = string.Format("data:image/jpeg;base64,{0}", imageBase64);
                        }
                        else
                        {
                            cartDto.Product.ImageUrl = null; // או תמונה ברירת מחדל
                        }
                    }
                }
                return Ok(shoppingCartDtos);
            }
            catch (Exception ex)
            {
                return BadRequest("cant get ShoppingCarts");
            }
        }
        // POST api/<ShoppingCartController>
        [HttpPost("{userId}")]
        public void addShoppingCart(int userId)
        {
            _shoppingCartService.addShoppingCart(userId);
        }
        // POST api/<ShoppingCartController>
        [HttpPost("addToCart/{userId}")]
        public ActionResult AddProduct(int userId, [FromBody] ShoppingCartModel shoppingCart)
        {
            try
            {
                if (shoppingCart == null || string.IsNullOrWhiteSpace(shoppingCart.Name))
                {
                    return BadRequest("שם המוצר חסר או שגוי.");
                }

                var product = _productService.GetAllProducts()
                    .FirstOrDefault(p => p.Name == shoppingCart.Name);

                if (product == null)
                {
                    return BadRequest("המוצר לא קיים.");
                }

                _shoppingCartService.AddProduct(userId, product);

                return Ok(new { message = "המוצר נוסף לסל בהצלחה." });
            }
            catch (Exception ex)
            {
                return BadRequest($"שגיאה בהוספת המוצר: {ex.Message}");
            }
        }

        // PUT api/<ShoppingCartController>/5
        [HttpPut("{userId}")]
        public ActionResult RemoveProduct(int userId, [FromBody] ShoppingCartModel shoppingCart)
        {
            try
            {
                var product = _productService.GetAllProducts()
                               .FirstOrDefault(p => p.Name == shoppingCart.Name);
                if (product == null)
                {
                    return BadRequest("Product not found");
                }
                _shoppingCartService.RemoveProduct(userId, product);
                return Ok(new { message = "RemoveProduct successfully" });
            }
            catch { return BadRequest("cant RemoveProduct"); }
        }
        // DELETE api/<ShoppingCartController>/5
        [HttpDelete("{userId}")]
        public void ClearCart(int userId)
        {
            _shoppingCartService.ClearCart(userId);
        }
        [HttpGet("getCheapestCart/{userId}")]
        public IActionResult CalculateCheapestCart(int userId)
        {
            return Ok(_shoppingCartService.CalculateCheapestCart(userId));
        }
    }
}
