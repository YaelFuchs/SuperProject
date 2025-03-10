using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Models
{
    public class PayPalOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Environment { get; set; } // "sandbox" או "live"
    }
}
