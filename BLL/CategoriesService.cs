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
            var categoryCheck = await _categoriesRepository.GetCategoryByNameAsync(categoryDto.Name);
            if (categoryCheck is not null){
                throw new InvalidOperationException($"Category with the name '{categoryDto.Name}' already exists with id '{categoryCheck.Id}'.");
            }
            return await _categoriesRepository.CreateCategoryAsync(categoryDto);
        }
        public async Task DeleteCategoryAsync(int id)
        {
            await _categoriesRepository.DeleteCategoryAsync(id);
        }
        public async Task UpdateCategoryAsync(UpdateCategoryCommandDto categoryDto)
        {
            var categoryCheck = await _categoriesRepository.GetCategoryByNameAsync(categoryDto.Name);
            if (categoryCheck is not null)
            {
                throw new InvalidOperationException($"Category with the name '{categoryDto.Name}' already exists with id '{categoryCheck.Id}'.");
            }
            await _categoriesRepository.UpdateCategoryAsync(categoryDto);
        }
    }
}
