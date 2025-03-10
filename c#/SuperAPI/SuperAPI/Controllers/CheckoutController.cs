using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaypalServerSdk.Standard;
using PaypalServerSdk.Standard.Controllers;
using PaypalServerSdk.Standard.Http.Response;
using PaypalServerSdk.Standard.Models;
using Super.Core.Models;
using Super.Core.Service;
using Super.Data;
using SuperAPI.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
namespace SuperAPI.Controllers
{
    [Authorize(Policy = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IPayPalService _paypalService;
        private readonly OrdersController _ordersController;
        private readonly PaymentsController _paymentsController;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly ILogger<CheckoutController> _logger;
        private readonly DataContext _context;
        public CheckoutController(PaypalServerSdkClient paypalClient, Microsoft.Extensions.Configuration.IConfiguration configuration, ILogger<CheckoutController> logger, IPayPalService payPalService, DataContext context)
        {
            _ordersController = paypalClient.OrdersController;
            _paymentsController = paypalClient.PaymentsController;
            _configuration = configuration;
            _logger = logger;
            _paypalService = payPalService;
            _context = context;
        }

        [HttpPost("orders")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderInputModel orderInput)
        {
            try
            {
                var result = await _CreateOrder(orderInput);

                _logger.LogInformation($"PayPal Create Order Response: {JsonConvert.SerializeObject(result)}"); // רשום את התגובה המלאה

                if (result.StatusCode == 201)
                {
                    var approveLink = result.Data.Links.FirstOrDefault(link => link.Rel == "approve");
                    if (approveLink != null)
                    {
                        return Ok(new { orderId = result.Data.Id, approveLink = approveLink.Href });
                    }
                    else
                    {
                        _logger.LogError("Approve link not found in PayPal response.");
                        return StatusCode(500, new { error = "Approve link not found." });
                    }
                }
                else
                {
                    _logger.LogError($"PayPal Create Order failed with status code: {result.StatusCode}");
                    return StatusCode((int)result.StatusCode, result.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create order");
                return StatusCode(500, new { error = "Failed to create order." });
            }
        }
        private async Task<ApiResponse<PaypalServerSdk.Standard.Models.Order>> _CreateOrder(OrderInputModel orderInput)
        {
            try
            {
                var intent = (CheckoutPaymentIntent)Enum.Parse(typeof(CheckoutPaymentIntent), "CAPTURE", true);

                var user = _context.Users.FirstOrDefault(u => u.Id == orderInput.CustomerId);
                if (user == null)
                {
                    return new ApiResponse<PaypalServerSdk.Standard.Models.Order>(
                        (int)System.Net.HttpStatusCode.BadRequest,
                        null,
                        null
                    );
                }
                var cart = _context.ShoppingCarts
                                .Where(c => c.UserId == user.Id)
                                .OrderByDescending(c => c.Id)
                                .Include(c => c.Carts)
                                .FirstOrDefault();

                var ordersCreateInput = new OrdersCreateInput
                {
                    Body = new OrderRequest
                    {
                        Intent = intent,
                        PurchaseUnits = new System.Collections.Generic.List<PurchaseUnitRequest>
                        {
                            new PurchaseUnitRequest
                            {
                                Amount = new AmountWithBreakdown { CurrencyCode = "ILS",MValue = orderInput.SumForPay.ToString("0.00", CultureInfo.InvariantCulture), },
                            },
                        },
                    },
                };
                var order = new Super.Core.Models.Order
                {
                    CustomerId = orderInput.CustomerId,
                    ShippingAddress = user.Address,
                    SumForPay = orderInput.SumForPay,
                    Currency = "ILS",
                    OrderDate = DateTime.UtcNow,
                    PaymentStatus = "Pending"
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return await _ordersController.OrdersCreateAsync(ordersCreateInput);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                throw;
            }
        }
        [HttpPost("orders/{orderID}/capture")]
        public async Task<IActionResult> CaptureOrder(string orderID)
        {
            try
            {
                var result = await _CaptureOrder(orderID);

                _logger.LogInformation($"PayPal Capture Order Response: {JsonConvert.SerializeObject(result)}"); // רשום את התגובה המלאה

                if (result.StatusCode == 201)
                {
                    var success = await _paypalService.ProcessPayment(orderID, 100, "USD");
                    if (success)
                    {
                        var order = _context.Orders.FirstOrDefault(o => o.Id == int.Parse(orderID));
                        if (order != null)
                        {
                            var cart = _context.ShoppingCarts
                                .Where(c => c.UserId == order.CustomerId)
                                .OrderByDescending(c => c.Id)
                                .Include(c => c.Carts)
                                .FirstOrDefault();

                            if (cart != null)
                            {
                                return Ok(new { order = result.Data, cart = cart });
                            }
                            else
                            {
                                return StatusCode((int)result.StatusCode, result.Data);
                            }
                        }
                        else
                        {
                            return StatusCode(500, new { error = "Order not found." });
                        }
                    }
                    else
                    {
                        return StatusCode(500, new { error = "Failed to process payment." });
                    }
                }
                else
                {
                    _logger.LogError($"PayPal Capture Order failed with status code: {result.StatusCode}");
                    return StatusCode((int)result.StatusCode, result.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to capture order");
                return StatusCode(500, new { error = "Failed to capture order." });
            }
        }
        private async Task<ApiResponse<PaypalServerSdk.Standard.Models.Order>> _CaptureOrder(string orderID)
        {
            try
            {
                var ordersCaptureInput = new OrdersCaptureInput { Id = orderID, };
                return await _ordersController.OrdersCaptureAsync(ordersCaptureInput);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error capturing order");
                throw;
            }
        }
    }
}
