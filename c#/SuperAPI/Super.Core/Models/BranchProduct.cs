using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Models
{
    public class BranchProduct
    {
        public int Id { get; set; }
        
        public Branch Branch { get; set; }
        public Product Product { get; set; }
        public double Price { get; set; }
        
    }
}
