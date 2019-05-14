using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BookStore.BusinessLogic.Interfaces;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetAllBooksAsync();

            if (books == null)
            {
                return NoContent();
            }

            return Ok(books);
        }

        [HttpGet("search/{title}")]
        public async Task<IActionResult> GetBooksByTitle(string title)
        {
            var books = await _bookService.GetBooksByTitle(title);

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
        public async Task<IActionResult> CreateBook([FromForm] IFormFile file, [FromForm] string jsonBook)
        {
            try
            {
                if (file.Length <= 0)
                {
                    return BadRequest();
                }

                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var book = JsonConvert.DeserializeObject<BookDto>(jsonBook);
                book.ImageUrl = Path.Combine(folderName, fileName);

                await _bookService.AddBookAsync(book);
                var d = book.Id;
                return Ok(new
                {
                    book.Id,
                    book.ImageUrl
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateBook([FromBody] CreateBookModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var book = new BookDto
        //        {
        //            ImageUrl = model.ImageUrl,
        //            Title = model.Title,
        //            Price = model.Price,
        //            Language = model.Language,
        //            PageSize = model.PageSize,
        //            Description = model.Description
        //        };
        //        await _bookService.AddBookAsync(book);
        //        return Ok();
        //    }

        //    return BadRequest(ModelState);
        //}

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new BookDto
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

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetBookByCategoryId(int id)
        {
            var book = await _bookService.GetBooksByCategoryId(id);

            if (book == null)
            {
                return NoContent();
            }

            return Ok(book);
        }
    }
}