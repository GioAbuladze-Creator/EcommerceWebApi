using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DAL.Infrastructure;
using Ecommerce.Shared.Abstractions;
using Ecommerce.Shared.Commands;
using Ecommerce.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL
{
    public class CategoriesRepository : ICategoriesRepository
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
        public async Task<CategoryFullInfo> GetCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with Id {id} not found.");
            }
            return MapToCategoryFullInfo(category);
        }
        public async Task<int> CreateCategoryAsync(CreateCategoryCommandDto categoryDto)
        {
            var category = new Category() { Name = categoryDto.Name };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null)
            {
                throw new KeyNotFoundException($"Category with Id {id} not found.");
            }
            _context.ProductCategories.RemoveRange(category.ProductCategories);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCategoryAsync(UpdateCategoryCommandDto categoryDto)
        {
            var category = await _context.Categories.FindAsync(categoryDto.Id);
            if (category is null)
            {
                throw new KeyNotFoundException($"Product with Id {categoryDto.Id} not found.");
            }
            if (!string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                category.Name = categoryDto.Name;
            }
            await _context.SaveChangesAsync();
        }
        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            var category = await _context.Categories
                   .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
            return category;
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
