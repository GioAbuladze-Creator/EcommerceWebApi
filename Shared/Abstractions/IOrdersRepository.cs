using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DAL;
using Ecommerce.Shared.Models;

namespace Ecommerce.Shared.Abstractions
{
    public interface IOrdersRepository
    {
        Task<List<Order>> GetUserOrdersAsync(int userId);
        Task<List<OrderDto>> GetUserOrdersDtoAsync(int userId);
        Task AddOrderFromCartAsync(Cart cart);
    }
}
