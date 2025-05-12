using Ecommerce.DAL;
using Ecommerce.Shared.Models;

namespace Ecommerce.Shared.Abstractions
{
    public interface IOrdersService
    {
        Task<List<Order>> GetUserOrdersAsync(int userId);
        Task<List<OrderDto>> GetUserOrdersDtoAsync(int userId);
        Task<List<OrderDto>> GetAllUsersOrdersDtoAsync();
        Task AddOrderFromCartAsync(Cart cart);
    }
}