using Super.Core.Models;

namespace SuperAPI.Models
{
    public class ProductPostModel
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public eUnitOfMeasure UnitOfMeasure { get; set; }
        public IFormFile? ImageUrl { get; set; }
    }
}
