using eShopSolution.Application.Catolog.Orders;
using eShopSolution.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderSevice)
        {
            _orderService = orderSevice;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CheckoutRequest checkoutRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderService.Create(checkoutRequest);
            if (order == 0)
            {
                return BadRequest(order);
            }
            return Ok(order);
        }
    }
}