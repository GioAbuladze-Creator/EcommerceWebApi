using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DAL;
using Ecommerce.Shared.Abstractions;
using Ecommerce.Shared.Models;

namespace Ecommerce.BLL
{
    public class OrdersService : IOrdersService
    {
        public readonly IOrdersRepository _ordersRepository;
        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        public async Task<List<Order>> GetUserOrdersAsync(int userId)
        {
            return await _ordersRepository.GetUserOrdersAsync(userId);
        }
        public async Task<List<OrderDto>> GetUserOrdersDtoAsync(int userId)
        {
            return await _ordersRepository.GetUserOrdersDtoAsync(userId);
        }
        public async Task AddOrderFromCartAsync(Cart cart)
        {
            for (int i = 0; i < cart.CartItems.Count; i++)
            {
                if (cart.CartItems[i].Product.Stock < cart.CartItems[i].Quantity)
                {
                    throw new InvalidOperationException($"Requested quantity ({cart.CartItems[i].Quantity}) exceeds available stock ({cart.CartItems[i].Product.Stock}).");
                }
            }
            await _ordersRepository.AddOrderFromCartAsync(cart);
        }


    }
}
