using Super.Core.Models;

namespace SuperAPI.Models
{
    public class ShoppingCartModel
    {
        public ProductPostModel Product { get; set; }
        public double Quantity { get; set; } 
    }
}
