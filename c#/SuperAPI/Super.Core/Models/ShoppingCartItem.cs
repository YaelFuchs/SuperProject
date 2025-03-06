using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; } // קישור לסל הקניות
        public int ProductId { get; set; }
        public Product Product { get; set; } // קישור למוצר
        public double Quantity { get; set; } // כמות (למשל 1.5 ק"ג)
    }
}

