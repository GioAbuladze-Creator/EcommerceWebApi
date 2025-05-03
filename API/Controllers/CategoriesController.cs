using Ecommerce.Shared.Abstractions;
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
    }
}
