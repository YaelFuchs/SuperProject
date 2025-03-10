using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Repositories
{
    public interface IPayPalRepository
    {
        Task<bool> ProcessPayment(string orderId, decimal sumForPay, string currency);

    }
}