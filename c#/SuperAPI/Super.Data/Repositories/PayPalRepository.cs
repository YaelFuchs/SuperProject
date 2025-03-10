using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PayPal.Api;
using Super.Core.Repositories;

namespace Super.Data.Repositories
{
    public class PayPalRepository: IPayPalRepository
    {
        private readonly APIContext _apiContext;
        public PayPalRepository()
        {
            var config = new Dictionary<string, string>
           {
               {"mode" ,"sanbox"}
        };
            string clientId = "AYMmEPI-ir78ehKjPsCx0zWsgnU7Up5Ht0ogLoOFQ4mxtZX5YzQk0MDF62Tko4SnpGVjEveM8l1lgcJB";
            string clientSecret = "EHd4dyz2v5HScyLwUG7cVjHbfWWVAP7BzNdf_2YPmUT4tCyy_5-RszrAIY-QZnQuxOipkjL_f7kIjXUo";

            var accessToken = new OAuthTokenCredential(clientId, clientSecret,config).GetAccessToken();
            _apiContext = new APIContext(accessToken) { Config = config };
        }
        public async Task<string> CreatePayment(double amount,string curency ,string returnUrl,string cancelUrl) {
            var payer = new Payer { payment_method = "paypal" };
            var transaction = new Transaction()
            {
                amount = new Amount()
                {
                    currency = curency,
                    total = amount.ToString("F2")
                },
                description = "תשלום באמצעות paypal"
            };
            var payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = new List<Transaction> { transaction },
                redirect_urls = new RedirectUrls()
                {
                    return_url = returnUrl,
                    cancel_url = cancelUrl
                }
            };
            var createPayment = payment.Create(_apiContext);
            return createPayment.links.Find(link => link.rel == "approval_url")?.href;
        }
        public async Task<Payment> ExecutePaymentAsync(string paymentId, string payerId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };

            try
            {
                var executedPayment = await Task.Run(() => payment.Execute(_apiContext, paymentExecution));
                return executedPayment;
            }
            catch (PayPal.HttpException ex)
            {
               
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void Success()
        {
           
        }
        public void Cancel()
        {

        }

        string IPayPalRepository.CreatePayment(double amount, string curency, string returnUrl, string cancelUrl)
        {
            throw new NotImplementedException();
        }
    }
}
