using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Shared.Commands;
using Ecommerce.Shared.Models;

namespace Ecommerce.Shared.Abstractions
{
    public interface ICategoriesService
    {
        Task<List<CategoryFullInfo>> GetCategoriesAsync();
        Task<CategoryFullInfo> GetCategoryAsync(int id);
        Task<int> CreateCategoryAsync(CreateCategoryCommandDto categoryDto);
        Task DeleteCategoryAsync(int id);
        Task UpdateCategoryAsync(UpdateCategoryCommandDto categoryDto);
    }
}
