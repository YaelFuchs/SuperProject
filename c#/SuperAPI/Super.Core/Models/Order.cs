using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentStatus { get; set; } = "Pending"; // הוסף ערך ברירת מחדל
        public decimal SumForPay { get; set; }
        public string Currency { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
