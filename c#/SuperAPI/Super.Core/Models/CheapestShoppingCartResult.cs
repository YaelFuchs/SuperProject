using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Models
{
    public class CheapestShoppingCartResult
    {
        public double BestCost { get; set; }
        public List<Branch> SelectedBranch { get; set; }
        public Dictionary<Product, Branch> ProductOrigins { get; set; }
    }
}


