using Ecommerce.Api.Extensions;
using Ecommerce.Shared.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly ICartsService _cartService;

        public OrdersController(IOrdersService ordersService, ICartsService cartsService)
        {
            _ordersService = ordersService;
            _cartService = cartsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCurrentUserOrders()
        {
            var userId = User.GetUserId();
            var orders = await _ordersService.GetUserOrdersDtoAsync(userId);

            return Ok(orders);
        }
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsersOrders()
        {
            var orders = await _ordersService.GetAllUsersOrdersDtoAsync();
            return Ok(orders);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersOrders(int id)
        {
            var orders = await _ordersService.GetUserOrdersDtoAsync(id);
            return Ok(orders);
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = User.GetUserId();
            var cart = await _cartService.GetCartAsync(userId);
            await _ordersService.AddOrderFromCartAsync(cart);
            return Ok(userId);
        }
    }
}
