using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Service
{
    public interface IPayPalService
    {
        Task<bool> ProcessPayment(string orderId, decimal sumForPay, string currency);

    }
}
