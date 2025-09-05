using ECommerceApp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private readonly OrderService _orderService;

        public PaymentController(PaymentService paymentService, OrderService orderService)
        {
            _paymentService = paymentService;
            _orderService = orderService;
        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay(int orderId, string method)
        {
            var order = await _orderService.GetOrderWithItemsAsync(orderId);
            if (order == null) return NotFound();

            var payment = await _paymentService.CreatePaymentAsync(order, method);
            return Ok(payment);
        }
    }
}
