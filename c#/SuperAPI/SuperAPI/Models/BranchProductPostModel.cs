using Super.Core.Models;

namespace SuperAPI.Models
{
    public class BranchProductPostModel
    {
        public int BranchId { get; set; }
        public int ProductId { get; set; } 
        public double Price { get; set; }
    }
}
