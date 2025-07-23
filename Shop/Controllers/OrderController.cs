using Application.OrderDto;
using Application.Use_Case;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderCase _orderCase;

        public OrderController(OrderCase orderCase)
        {
            _orderCase = orderCase;
        }

        [HttpPost]

        public async Task<ActionResult> CreateOrder([FromBody] CreateOrderDto orderData)
        {
            if (orderData == null || orderData.UserId == Guid.Empty) 
            {
                return BadRequest();
            }

            var order = await _orderCase.CreateOrder(orderData);

            if (order == null)
            {
                return BadRequest("не удвлось создать заказ");
            
            }

            return Ok(order);
        }

        [HttpGet]

        public async Task<ActionResult<List<GetOrdersDto>>> GetAllUserOrders(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest();
            }

            var orders = await _orderCase.GetAllUserOrders(userId);

            if (orders == null)
            {
                return NotFound();
            } 

            return Ok(orders);
        }

        [HttpPut]

        public async Task<ActionResult<GetOrdersDto>> ChangeStatusOrders(Guid userId, byte status)
        {
                var change = await _orderCase.ChangeStatusOrder(userId, status);

                return Ok(change);
        }
    }
}
