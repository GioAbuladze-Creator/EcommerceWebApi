using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Shared.Abstractions;
using Ecommerce.Shared.Commands;
using Ecommerce.Shared.Models;

namespace Ecommerce.BLL
{
    public class CartsService : ICartsService
    {
        private readonly ICartsRepository _cartsRepository;
        public CartsService(ICartsRepository cartsRepository)
        {
            _cartsRepository = cartsRepository;
        }
        public async Task<Cart> GetCartAsync(int userId)
        {
            var cart = await _cartsRepository.GetCartAsync(userId);
            return cart;
        }
        public async Task<CartDto> GetCartFullInfoAsync(int userId)
        {
            var cart = await _cartsRepository.GetCartFullInfoAsync(userId);
            return cart;
        }

        public async Task<int> AddToCartAsync(Product product, Cart cart, AddToCartCommandDto item)
        {
            var productCheck = _cartsRepository.GetCartItem(product.Id, cart);
            if (productCheck is not null)
            {
                QuantityCheck(productCheck.Quantity + item.Quantity, product.Stock);
                await _cartsRepository.UpdateToCartAsync(productCheck, productCheck.Quantity + item.Quantity);
                return productCheck.CartItemId;
            }
            QuantityCheck(item.Quantity, product.Stock);
            return await _cartsRepository.AddToCartAsync(product, cart, item);
        }
        public async Task UpdateToCartAsync(CartItem cartItem, int quantity)
        {
            QuantityCheck(quantity, cartItem.Product.Stock);

            await _cartsRepository.UpdateToCartAsync(cartItem, quantity);
        }
        public void QuantityCheck(int quantity, int stock)
        {
            if (quantity <= 0)
            {
                throw new InvalidOperationException($"Requested quantity ({quantity}) is less than 1.");
            }
            if (stock < quantity)
            {
                throw new InvalidOperationException($"Requested quantity ({quantity}) exceeds available stock ({stock}).");
            }
        }
        public CartItem? GetCartItem(int productId, Cart cart)
        {
            return _cartsRepository.GetCartItem(productId, cart);
        }
    }
}
