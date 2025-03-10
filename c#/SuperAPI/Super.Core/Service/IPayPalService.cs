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
        public string CreatePayment(double amount, string curency, string returnUrl, string cancelUrl);
        Task<Payment> ExecutePaymentAsync(string paymentId, string payerId); // הוספתי פונקציה

        public void Success();
        public void Cancel();






            }
}
