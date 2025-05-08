using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Shared.Commands;
using Ecommerce.Shared.Models;

namespace Ecommerce.Shared.Abstractions
{
    public interface ICartsRepository
    {
        Task<Cart> GetCartAsync(int userId);
        Task<CartDto> GetCartFullInfoAsync(int userId);
        Task<int> AddToCartAsync(Product product, Cart cart, AddToCartCommandDto item);
        Task UpdateToCartAsync(CartItem cartItem, int quantity);
        Task RemoveItemFromCartAsync(int cartItemId);
        Task ClearCartAsync(Cart cart);
        CartItem GetCartItem(int productId, Cart cart);
    }
}
