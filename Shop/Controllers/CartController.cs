using Application.CartDto;
using Application.Use_Case;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartCase _cartCase;

       public CartController(CartCase cartCase)
        {
            _cartCase = cartCase;
        }

        [HttpGet("{id:guid}")]

        public async Task<ActionResult<getCartDto>> getCartrByUserId(System.Guid id)
        {
            var cart = await _cartCase.getCartByUserId(id);

            if (cart == null || id == Guid.Empty)
            {
                return BadRequest();
            }

          
            return Ok(cart);
        }


        [HttpPost("AddItemInCart")]

        public async Task<ActionResult<CartItemDto>> addItemInCart(Guid userId ,[FromBody] ResponseItemDto item)
        {
            if (userId == Guid.Empty) 
            {
                return BadRequest("Id is Empty");
            }
            if (item == null)
            {
                return BadRequest(item);
            }

            var cartItem = await _cartCase.addItemInCart(userId, item);

            return NoContent();  
        }

        [HttpPost("removeCartItem")]

        public async Task<ActionResult<CartItemDto>> removeItemInCart(Guid userId, [FromBody] ResponseItemDto cartItem)
        {
            if(userId == Guid.Empty)
            {
                return BadRequest("id is empty");

            }
            if(cartItem == null)
            {
                return BadRequest();
            }

            var deletedItem = await _cartCase.removeItemInCart(userId, cartItem);

            if (deletedItem == null)
            {
                return BadRequest("this item dont exist");
            }

            return NoContent();
        }
    }
}
