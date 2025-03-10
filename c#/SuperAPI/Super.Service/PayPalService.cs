using PayPal.Api;
using Super.Core.Repositories;
using Super.Core.Service;
using System.Threading.Tasks;

namespace Super.Service
{
    public class PayPalService : IPayPalService
    {
        private readonly IPayPalRepository _payPalRepository;

        public PayPalService(IPayPalRepository payPalRepository)
        {
            _payPalRepository = payPalRepository;
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public string CreatePayment(decimal amount, string curency, string returnUrl, string cancelUrl)
        {
            throw new NotImplementedException();
        }

        public Task<Payment> ExecutePaymentAsync(string paymentId, string payerId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ProcessPayment(string orderId, decimal sumForPay, string currency)
        {
            // לוגיקה עסקית: עדכון מסד נתונים, שליחת אימיילים, וכו'
            // ...
            return await _payPalRepository.ProcessPayment(orderId, sumForPay, currency);
        }

        public void Success()
        {
            throw new NotImplementedException();
        }
    }
}