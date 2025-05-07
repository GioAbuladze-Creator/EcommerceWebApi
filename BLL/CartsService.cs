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
        public async Task<CartFullInfo> GetCartFullInfoAsync(int userId)
        {
            var cart = await _cartsRepository.GetCartFullInfoAsync(userId);
            return cart;
        }

        public async Task<int> AddToCartAsync(Cart cart, AddToCartCommandDto item)
        {
            return await _cartsRepository.AddToCartAsync(cart, item);
        }


    }
}
