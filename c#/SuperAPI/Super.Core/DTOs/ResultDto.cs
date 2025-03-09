using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.DTOs
{
    public class ResultDto
    {
        public List<ProductPriceDto> Prices { get; set; }   
        public CheapestShoppingCartResult CheapestShoppingCartResult { get; set; }
    }
}
