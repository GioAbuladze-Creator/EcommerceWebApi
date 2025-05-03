using Ecommerce.Shared.Commands;
using Ecommerce.Shared.Models;

namespace Ecommerce.Shared.Abstractions
{
    public interface ICategoriesRepository
    {
        Task<List<CategoryFullInfo>> GetCategoriesAsync();
        Task<CategoryFullInfo> GetCategoryAsync(int id);
        Task<int> CreateCategoryAsync(CreateCategoryCommandDto categoryDto);
        Task DeleteCategoryAsync(int id);
        Task UpdateCategoryAsync(UpdateCategoryCommandDto categoryDto);
        Task<Category?> GetCategoryByNameAsync(string name);
    }
}