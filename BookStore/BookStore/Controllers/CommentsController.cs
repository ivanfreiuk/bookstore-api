using System.Threading.Tasks;
using BookStore.BusinessLogic.Interfaces;
using BookStore.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentService.GetAllCommentsAsync();

            if (comments == null)
            {
                return NoContent();
            }

            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _commentService.GetCommentAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentDto comment)
        {
            if (ModelState.IsValid)
            {
                await _commentService.AddCommentAsync(comment);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] CommentDto comment)
        {
            if (ModelState.IsValid)
            {
                await _commentService.AddCommentAsync(comment);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentService.GetCommentAsync(id);

            if (comment == null)
            {
                return BadRequest();
            }

            await _commentService.RemoveCommentAsync(id);

            return Ok(comment);
        }

        [HttpGet("book/{id}")]
        public async Task<IActionResult> GetCommentsByBookId(int id)
        {
            var comments = await _commentService.GetCommentsByBookIdAsync(id);

            if (comments == null)
            {
                return NoContent();
            }

            return Ok(comments);
        }
    }
}
