using Super.Core.Models;

namespace SuperAPI.Models
{
    public class ProductPostModel
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public eUnitOfMeasure UnitOfMeasure { get; set; }
    }
}
