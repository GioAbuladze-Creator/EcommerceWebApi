using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DAL.Infrastructure;
using Ecommerce.Shared.Abstractions;
using Ecommerce.Shared.Commands;
using Ecommerce.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL
{
    public class CartsRepository : ICartsRepository
    {
        private readonly ProductsDbContext _context;
        public CartsRepository(ProductsDbContext productsContext)
        {
            _context = productsContext;
        }
        public async Task<Cart> GetCartAsync(int userId)
        {
            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart is null)
            {
                cart = new Cart();
            }
            return cart;
        }
        public async Task<CartDto> GetCartFullInfoAsync(int userId)
        {
            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart is null)
            {
                cart = new Cart();
            }
            return new CartDto()
            {
                CartId = cart.CartId,
                UserId = userId,
                CreatedAt = cart.CreatedAt,
                CartItems = cart.CartItems.ToDictionary(ci => ci.CartItemId, ci => new CartItemDto()
                {
                    ProductId = ci.ProductId,
                    Price = ci.Product.Price,
                    ProductName = ci.Product.Name,
                    Quantity = ci.Quantity,
                    Total = ci.Quantity * float.Parse(ci.Product.Price)
                })
            };
        }
        public async Task<int> AddToCartAsync(Product product, Cart cart, AddToCartCommandDto item)
        {
            var cartItem = new CartItem
            {
                CartId = cart.CartId,
                Product = product,
                Quantity = item.Quantity
            };
            await _context.AddAsync(cartItem);
            await _context.SaveChangesAsync();

            return cartItem.CartItemId;
        }
        public async Task UpdateToCartAsync(CartItem cartItem, int quantity)
        {
            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();
        }
        public async Task RemoveItemFromCartAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem is null)
            {
                throw new KeyNotFoundException($"Cart Item with Id {cartItemId} not found.");
            }
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
        public async Task ClearCartAsync(Cart cart)
        {
            cart.CartItems.Clear();
            await _context.SaveChangesAsync();
        }
        public CartItem? GetCartItem(int productId, Cart cart)
        {
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            return cartItem;
        }
    }


}
