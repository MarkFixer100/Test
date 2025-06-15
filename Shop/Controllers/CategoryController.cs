using Application.CategoryDto;
using Application.ProductDto;
using Application.Use_Case;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryCase _categoryCase;
        public CategoryController(CategoryCase categoryCase ) { 
        
            _categoryCase = categoryCase;
        
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<CategoryDtos>>> GetAll()
        {
            IEnumerable<CategoryDtos> categories = await _categoryCase.getAllCategories();

            return Ok(categories);

        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryDtos>> GetById(Guid id)
        {
            var category = await _categoryCase.getById(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDtos>> Create(CreateCategoryDtos model)
        {
            var created = await _categoryCase.CreateCategoriesAsync(model);

            return Ok(created);
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryCase.Delete(id);
            return NoContent();
        }
    }
}
