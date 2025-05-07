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
        public async Task<CartFullInfo> GetCartFullInfoAsync(int userId)
        {
            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart is null)
            {
                cart = new Cart();
            }
            return new CartFullInfo()
            {
                CartId = cart.CartId,
                UserId = userId,
                CreatedAt = cart.CreatedAt,
                CartItems = cart.CartItems.ToDictionary(ci => ci.CartItemId, ci => new CartItemFullInfo()
                {
                    ProductId = ci.ProductId,
                    Price = ci.Product.Price,
                    ProductName = ci.Product.Name,
                    Quantity = ci.Quantity,
                    Total=ci.Quantity*float.Parse(ci.Product.Price)
                })
            };

        }
        public async Task<int> AddToCartAsync(Cart cart, AddToCartCommandDto item)
        {
            var product = await _context.Products.FindAsync(item.ProductId);
            if (product is null)
            {
                throw new KeyNotFoundException($"Product with Id {item.ProductId} not found.");
            }
            // biznes biznes!!!!!!!!!!
            if (product.Stock < item.Quantity)
            {
                throw new InvalidOperationException($"Requested quantity ({item.Quantity}) exceeds available stock ({product.Stock}).");
            }
            var productCheck = cart.CartItems.FirstOrDefault(ci => ci.ProductId == item.ProductId);

            if (productCheck is not null)
            {
                productCheck.Quantity += item.Quantity;
                await _context.SaveChangesAsync();
                return productCheck.CartItemId;
            }
            else
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
        }
    }

    
}
