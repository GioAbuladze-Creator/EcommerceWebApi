using Ecommerce.Shared.Abstractions;
using Ecommerce.Shared.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoriesService.GetCategoriesAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoriesService.GetCategoryAsync(id);
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommandDto categoryDto)
        {
            var categoryId = await _categoriesService.CreateCategoryAsync(categoryDto);
            return Ok(categoryId);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoriesService.DeleteCategoryAsync(id);
            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryCommandDto categoryDto)
        {
            categoryDto.Id = id;
            await _categoriesService.UpdateCategoryAsync(categoryDto);
            return Ok(categoryDto);
        }
    }
}
