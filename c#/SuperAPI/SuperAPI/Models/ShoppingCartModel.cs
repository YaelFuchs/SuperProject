using Super.Core.Models;

namespace SuperAPI.Models
{
    public class ShoppingCartModel
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public eUnitOfMeasure UnitOfMeasure { get; set; }

    }
}
