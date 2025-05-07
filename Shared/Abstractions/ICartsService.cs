using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Shared.Commands;
using Ecommerce.Shared.Models;

namespace Ecommerce.Shared.Abstractions
{
    public interface ICartsService
    {
        Task<Cart> GetCartAsync(int userId);
        Task<CartFullInfo> GetCartFullInfoAsync(int userId);
        Task<int> AddToCartAsync(Cart cart, AddToCartCommandDto item);
    }
}
