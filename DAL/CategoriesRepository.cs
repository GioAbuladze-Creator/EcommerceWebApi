using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DAL.Infrastructure;
using Ecommerce.Shared.Abstractions;
using Ecommerce.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL
{
    public class CategoriesRepository:ICategoriesRepository
    {
        private readonly ProductsDbContext _context;
        public CategoriesRepository(ProductsDbContext productsDbContext)
        {
            _context = productsDbContext; 
        }
        public async Task<List<CategoryFullInfo>> GetCategoriesAsync()
        {
            var categories = await _context.Categories
               .ToListAsync();
            return categories.Select(MapToCategoryFullInfo).ToList();
        }
        private CategoryFullInfo MapToCategoryFullInfo(Category category)
        {
            return new CategoryFullInfo()
            {
                Id = category.Id,
                Name = category.Name,
                Products = (category.ProductCategories ?? new List<ProductCategory>())
                    .ToDictionary(pc => pc.ProductId, pc => pc.Product.Name)
            };
        }
    }
}
