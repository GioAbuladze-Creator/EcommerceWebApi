using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Shared.Abstractions;
using Ecommerce.Shared.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService, ICategoriesService categoriesService)
        {
            _productsService = productsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productsService.GetProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productsService.GetProductDtoAsync(id);
            return Ok(product);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct(CreateProductCommandDto product)
        {
            var productId = await _productsService.CreateProductAsync(product);
            return Ok(productId);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productsService.DeleteProductAsync(id);
            return Ok(id);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductCommandDto productDto)
        {
            productDto.Id = id;
            await _productsService.UpdateProductAsync(productDto);
            return Ok(id);
        }
    }
}
