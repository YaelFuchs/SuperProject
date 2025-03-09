using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.DTOs
{
    public class ProductPriceDto
    {
        public Product Product { get; set; }
        public double Price { get; set; }
    }
}
