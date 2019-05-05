using System.Threading.Tasks;
using BookStore.BusinessLogic.Interfaces;
using BookStore.DataAccess.Entities;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class  BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetAllBooksAsync();

            if (books == null)
            {
                return NoContent();
            }

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBookAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    ImageUrl = model.ImageUrl,
                    Title = model.Title,
                    Price = model.Price,
                    Language = model.Language,
                    PageSize = model.PageSize,
                    Description = model.Description
                };
                await _bookService.AddBookAsync(book);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    //ImageUrl = model.ImageUrl,
                    //Title = model.Title,
                    //Price = model.Price,
                    //Language = model.Language,
                    //PageSize = model.PageSize,
                    //Description = model.Description
                };
                await _bookService.UpdateBookAsync(book);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookService.GetBookAsync(id);

            if (book == null)
            {
                return BadRequest();
            }

            await _bookService.RemoveBookAsync(id);

            return Ok(book);
        }
    }
}