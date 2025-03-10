using PayPal.Api;
using Super.Core.Repositories;
using Super.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Service
{
    public class PayPalService: IPayPalService
    {
        private readonly IPayPalRepository _payPalRepository;
        public PayPalService(IPayPalRepository payPalRepository)
        {
            _payPalRepository = payPalRepository;
        }
        public string CreatePayment(double amount, string curency, string returnUrl, string cancelUrl)
        {
           return _payPalRepository.CreatePayment(amount, curency, returnUrl, cancelUrl);
        }
        public async Task<Payment> ExecutePaymentAsync(string paymentId, string payerId)
        {
            return await _payPalRepository.ExecutePaymentAsync(paymentId, payerId);
        }
        public void Cancel()
        {

        }
        public void Success()
        {

        }

    }
}
