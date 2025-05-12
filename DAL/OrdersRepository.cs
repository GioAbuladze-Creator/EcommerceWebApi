using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DAL.Extensions;
using Ecommerce.DAL.Infrastructure;
using Ecommerce.Shared.Abstractions;
using Ecommerce.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL
{
    public class OrdersRepository : IOrdersRepository
    {
        public readonly ProductsDbContext _context;
        public OrdersRepository(ProductsDbContext ProductsDbContext)
        {
            _context = ProductsDbContext;
        }
        public async Task<List<Order>> GetUserOrdersAsync(int userId)
        {
            var orders = await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
            return orders;
        }
        public async Task<List<OrderDto>> GetUserOrdersDtoAsync(int userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
            var orderDtos = MapToOrdersDtos(orders);

            return orderDtos;
        }
        public async Task<List<OrderDto>> GetAllUsersOrdersDtoAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            var orderDtos = MapToOrdersDtos(orders);
            return orderDtos;
        }
        public async Task AddOrderFromCartAsync(Cart cart)
        {
            var order = MapCartToOrder(cart);
            _context.Orders.Add(order);
            cart.CartItems.Clear();
            await _context.SaveChangesAsync();
        }
        private Order MapCartToOrder(Cart cart)
        {
            return new Order()
            {
                UserId = cart.UserId,
                Status = Status.Completed,
                OrderItems = cart.CartItems.Select(ci => MapCartItemToOrderItem(ci)).ToList(),
                TotalAmount = cart.CartItems.Sum(ci => float.Parse(ci.Product.Price) * ci.Quantity)
            };
        }
        private OrderItem MapCartItemToOrderItem(CartItem cartItem)
        {
            cartItem.Product.Stock -= cartItem.Quantity;

            return new OrderItem()
            {
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                Product = cartItem.Product,
            };
        }
        private List<OrderDto> MapToOrdersDtos(List<Order> orders)
        {
            var orderDtos = orders.Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                CreatedAt = o.CreatedAt,
                OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                {
                    OrderId = o.OrderId,
                    OrderItemId = oi.OrderItemId,
                    Quantity = oi.Quantity,
                    ProductId = oi.ProductId,
                    Product = oi.Product.ToDto()
                }).ToList()
            }).ToList();
            return orderDtos;
        }
    }
}
