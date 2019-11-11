using System;
using System.Threading.Tasks;
using BookStore.BusinessLogic.Interfaces;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();

            if (authors == null)
            {
                return NoContent();
            }

            return Ok(authors);
        }


        [HttpGet("{id}", Name = "AuthorById")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto author)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                await _authorService.AddAuthorAsync(author);

                return CreatedAtRoute("CategoryById", new { id = author.Id }, author);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDto author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var authorDto = await _authorService.GetAuthorAsync(id);

                    if (authorDto == null)
                    {
                        return NotFound();
                    }

                    await _authorService.UpdateAuthorAsync(author);
                    return Ok();
                }

                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryFromDb = await _authorService.GetAuthorAsync(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            await _authorService.RemoveAuthorAsync(id);
            return Ok();
        }
    }
}