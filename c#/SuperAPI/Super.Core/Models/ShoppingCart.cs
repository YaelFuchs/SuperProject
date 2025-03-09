using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        // במקום Dictionary, רשימה של פריטים בסל
        public List<ShoppingCartItem> Carts { get; set; } = new List<ShoppingCartItem>();

    }
}
