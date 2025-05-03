using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Shared.Abstractions;
using Ecommerce.Shared.Models;

namespace Ecommerce.BLL
{
    public class CategoriesService:ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoriesService(ICategoriesRepository categoriesRepository) {
            _categoriesRepository = categoriesRepository;
        }
        public async Task<List<CategoryFullInfo>> GetCategoriesAsync()
        {
            return await _categoriesRepository.GetCategoriesAsync();
        }

    }
}
