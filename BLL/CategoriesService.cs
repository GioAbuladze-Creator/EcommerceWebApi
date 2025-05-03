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
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }
        public async Task<List<CategoryFullInfo>> GetCategoriesAsync()
        {
            return await _categoriesRepository.GetCategoriesAsync();
        }
        public async Task<CategoryFullInfo> GetCategoryAsync(int id)
        {
            return await _categoriesRepository.GetCategoryAsync(id);
        }
        public async Task<int> CreateCategoryAsync(CreateCategoryCommandDto categoryDto)
        {
            return await _categoriesRepository.CreateCategoryAsync(categoryDto);
        }
        public async Task DeleteCategoryAsync(int id)
        {
            await _categoriesRepository.DeleteCategoryAsync(id);
        }
        public async Task UpdateCategoryAsync(UpdateCategoryCommandDto categoryDto)
        {
            await _categoriesRepository.UpdateCategoryAsync(categoryDto);
        }
    }
}
