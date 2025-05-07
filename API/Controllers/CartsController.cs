using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ecommerce.Shared.Abstractions;
using Ecommerce.Shared.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartsService _cartsService;
        public CartsController(ICartsService cartsService)
        {
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

            int itemId = await _cartsService.AddToCartAsync(cart,item);
            return Ok(itemId);
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
