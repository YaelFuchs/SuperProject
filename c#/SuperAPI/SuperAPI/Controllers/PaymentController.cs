using Microsoft.AspNetCore.Mvc;
using Super.Core.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayPalService _payPalService;
        private readonly ILogger<PaymentController> _logger; // הוסף שדה פרטי

        public PaymentController(IPayPalService payPalService, ILogger<PaymentController> logger)
        {
            _payPalService = payPalService;
            _logger = logger;

        }

        // GET: api/<PaymentController>
        [HttpGet]
        public IActionResult CreatePayment()
        {
            string returnUrl = "http://localhost:4200/payment/success";
            string cancelUrl = "http://localhost:4200/payment/cancel";

            string approvalUrl = _payPalService.CreatePayment(10.00, "USD", returnUrl, cancelUrl);
            if(approvalUrl !=null)
            {
                return Ok(new  {url = approvalUrl});

            }
            return BadRequest("שגיאה ביצירת תשלום");
        }

        // GET api/<PaymentController>/5
        [HttpGet("success")]
        public async Task<IActionResult> Success(string paymentId, string PayerID)
        {
            try
            {
                var executedPayment = await _payPalService.ExecutePaymentAsync(paymentId, PayerID);

                if (executedPayment != null && executedPayment.state.ToLower() == "approved")
                {
                    _logger.LogInformation($"התשלום עבר בהצלחה. paymentId: {paymentId}, PayerID: {PayerID}");

                    // כאן תוסיף לוגיקה לעדכון סטטוס ההזמנה במסד הנתונים
                    // וטיפול בלוגיקה עסקית נוספת

                    return Ok("התשלום עבר בהצלחה");
                }
                else
                {
                    _logger.LogError($"אימות תשלום נכשל. paymentId: {paymentId}, PayerID: {PayerID}");
                    return BadRequest("אימות תשלום נכשל");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"שגיאה באימות תשלום. paymentId: {paymentId}, PayerID: {PayerID}");
                return BadRequest($"שגיאה באימות תשלום: {ex.Message}");
            }
        }

        [HttpGet("cancel")]
        public IActionResult Cancel()
        {
            return Ok("התשלום בוטל");
        }

      

       
    }
}
