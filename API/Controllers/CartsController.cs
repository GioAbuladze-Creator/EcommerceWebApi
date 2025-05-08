using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ecommerce.BLL;
using Ecommerce.Shared.Abstractions;
using Ecommerce.Shared.Commands;
using Ecommerce.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly ICartsService _cartsService;
        public CartsController(IProductsService productsService, ICartsService cartsService)
        {
            _productsService = productsService;
            _cartsService = cartsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserId();

            var cart = await _cartsService.GetCartFullInfoAsync(userId);

            return Ok(cart);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(AddToCartCommandDto item)
        {
            var userId = GetUserId();

            var cart = await _cartsService.GetCartAsync(userId);
            var product = await _productsService.GetProductAsync(item.ProductId);
            int cartItemId = await _cartsService.AddToCartAsync(product, cart, item);

            return Ok(cartItemId);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToCart(int id, int quantity)
        {
            var userId = GetUserId();
            var cart = await _cartsService.GetCartAsync(userId);
            var cartItem = _cartsService.GetCartItem(id, cart);
            if (cartItem == null)
            {
                throw new KeyNotFoundException($"Product with Id {id} is not in the Cart.");
            }
            await _cartsService.UpdateToCartAsync(cartItem, quantity);
            return Ok(cartItem.CartItemId);
        }
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveItemFromCart(int cartItemId)
        {
            await _cartsService.RemoveItemFromCartAsync(cartItemId);
            return Ok(cartItemId);
        }
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = GetUserId();
            var cart = await _cartsService.GetCartAsync(userId);
            await _cartsService.ClearCartAsync(cart);
            return Ok(cart.CartId);
        }

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User must be signed in to perform this action.");
            }
            var userId = int.Parse(userIdClaim.Value);
            return userId;
        }
    }
}
