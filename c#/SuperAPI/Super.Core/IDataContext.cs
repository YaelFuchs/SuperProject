using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core
{
    public interface IDataContext
    {
        List<User> Users { get; set; }
        
        List<Product> Products { get; set; }
        
        List<Category> Categories { get; set; }
        List<BranchProduct> BranchProducts { get; set; }
        List<Branch> Branches { get; set; }
    }
}
