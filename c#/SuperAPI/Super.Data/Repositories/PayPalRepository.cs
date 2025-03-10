using Super.Core.Models;
using Super.Core.Repositories;
using Super.Data;
using System.Threading.Tasks;

namespace Super.Data.Repositories
{
    public class PayPalRepository : IPayPalRepository
    {
        private readonly DataContext _context;

        public PayPalRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> ProcessPayment(string orderId, decimal sumForPay, string currency)
        {
            try
            {
                var order = _context.Orders.FirstOrDefault(o => o.Id == int.Parse(orderId));

                if (order == null)
                {
                    return false;
                }

                order.PaymentStatus = "Paid"; // עדכן את סטטוס התשלום
                order.SumForPay = sumForPay; // עדכן סכום
                order.Currency = currency;   // עדכן מטבע

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // הוסף לוגיקה לטיפול בשגיאות
                return false;
            }
        }


    }
}