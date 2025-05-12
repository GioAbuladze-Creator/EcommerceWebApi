using Microsoft.EntityFrameworkCore;
using Ecommerce.DAL.Infrastructure;
using Ecommerce.Shared.Abstractions;
using Ecommerce.Shared.Commands;
using Ecommerce.Shared.Models;
using Ecommerce.DAL.Extensions;

namespace Ecommerce.DAL
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductsDbContext _context;
        public ProductsRepository(ProductsDbContext productsDbContext)
        {
            _context = productsDbContext;
        }
        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var products = await _context.Products
                .ToListAsync();
            return products.Select(p=>p.ToDto()).ToList();
        }
        public async Task<Product> GetProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product is null)
            {
                throw new KeyNotFoundException($"Product with Id {productId} not found.");
            }
            return product;
        }
        public async Task<ProductDto> GetProductDtoAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product is null)
            {
                throw new KeyNotFoundException($"Product with Id {productId} not found.");
            }
            return product.ToDto();
        }
        public async Task<int> CreateProductAsync(CreateProductCommandDto productDto)
        {
            var finalProduct = new Product()
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
            };
            await _context.Products.AddAsync(finalProduct);
            await _context.SaveChangesAsync();
            return finalProduct.Id;
        }
        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
            {
                throw new KeyNotFoundException($"Product with Id {id} not found.");
            }
            _context.ProductCategories.RemoveRange(product.ProductCategories);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProductAsync(UpdateProductCommandDto productDto)
        {
            var product = await _context.Products.FindAsync(productDto.Id);
            if (product is null)
            {
                throw new KeyNotFoundException($"Product with Id {productDto.Id} not found.");
            }
            if (!string.IsNullOrWhiteSpace(productDto.Name))
            {
                product.Name = productDto.Name;
            }
            if (!string.IsNullOrWhiteSpace(productDto.Description))
            {
                product.Description = productDto.Description;
            }
            if (!string.IsNullOrWhiteSpace(productDto.Price))
            {
                product.Price = productDto.Price;
            }
            if (productDto.Stock is not null)
            {
                product.Stock = productDto.Stock.Value;
            }
            await _context.SaveChangesAsync();
        }
    }
}
