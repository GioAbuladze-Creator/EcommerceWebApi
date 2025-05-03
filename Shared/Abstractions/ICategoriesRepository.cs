using Ecommerce.Shared.Models;

namespace Ecommerce.Shared.Abstractions
{
    public interface ICategoriesRepository
    {
        Task<List<CategoryFullInfo>> GetCategoriesAsync();
    }
}