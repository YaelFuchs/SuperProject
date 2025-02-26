using Super.Core.Models;

namespace SuperAPI.Models
{
    public class BranchProductPostModel
    {
        public Branch Branch { get; set; }
        public Product Product { get; set; }
        public double Price { get; set; }
    }
}
