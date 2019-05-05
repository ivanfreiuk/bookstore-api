using System.Threading.Tasks;
using BookStore.BusinessLogic.Interfaces;
using BookStore.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (categories == null)
            {
                return NoContent();
            }

            return Ok(categories);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategoryAsync(category);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto category)
        {
            if (ModelState.IsValid)
            {
                var categoryFromDb = await _categoryService.GetCategoryAsync(id);

                if (categoryFromDb == null)
                {
                    return NotFound();
                }

                await _categoryService.UpdateCategoryAsync(category);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryFromDb = await _categoryService.GetCategoryAsync(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            await _categoryService.RemoveCategoryAsync(id);
            return Ok();
        }
    }
}
