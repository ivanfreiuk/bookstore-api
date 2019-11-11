using System;
using System.Threading.Tasks;
using BookStore.BusinessLogic.Interfaces;
using BookStore.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/cartitems")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemsController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            var cartItems = await _cartItemService.GetAllCartItemsAsync();

            if (cartItems == null)
            {
                return NoContent();
            }

            return Ok(cartItems);
        }


        [HttpGet("{id}", Name = "GetCartItemById")]
        public async Task<IActionResult> GetCartItem(int id)
        {
            try
            {
                var cartItems = await _cartItemService.GetCartItemAsync(id);

                if (cartItems == null)
                {
                    return NotFound();
                }

                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCartItemsByUserId(int userId)
        {
            try
            {
                var cartItems = await _cartItemService.GetCartItemsByUserId(userId);

                if (cartItems == null)
                {
                    return NoContent();
                }

                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateCartItem([FromBody] CartItemDto cartItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                await _cartItemService.AddCartItemAsync(cartItem);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, [FromBody] CartItemDto cartItem)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var cartItemFromDb = await _cartItemService.GetCartItemAsync(id);
                if (cartItemFromDb == null)
                {
                    await _cartItemService.AddCartItemAsync(cartItem);

                    return CreatedAtRoute("GetCartItemById", cartItem.Id, cartItem);
                }

                await _cartItemService.UpdateCartItemAsync(cartItem);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            try
            {
                var categoryFromDb = await _cartItemService.GetCartItemAsync(id);

                if (categoryFromDb == null)
                {
                    return NotFound();
                }

                await _cartItemService.RemoveCartItemAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}