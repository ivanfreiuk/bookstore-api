using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BusinessLogic.Interfaces;
using BookStore.BusinessLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/wishes")]
    [ApiController]
    public class WishesController : ControllerBase
    {
        private readonly IWishService _wishService;

        public WishesController(IWishService wishService)
        {
            _wishService = wishService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWishes()
        {
            var wishes = await _wishService.GetAllWishesAsync();

            if (wishes == null)
            {
                return NoContent();
            }

            return Ok(wishes);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetWishesByUserId(int userId)
        {
            var wishes = await _wishService.GetWishesByUserIdAsync(userId);

            if (wishes == null)
            {
                return NoContent();
            }

            return Ok(wishes);
        }

        [HttpPost]
        public async Task<IActionResult> AddWish([FromBody] WishDto wish)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                await _wishService.AddWishAsync(wish);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpDelete("{userId}/{bookId}")]
        public async Task<IActionResult> DeleteCategory(int userId, int bookId)
        {
            var wishFromDb = await _wishService.GetWishAsync(userId, bookId);

            if (wishFromDb == null)
            {
                return NotFound();
            }

            await _wishService.RemoveWishAsync(userId, bookId);
            return Ok();
        }
    }
}