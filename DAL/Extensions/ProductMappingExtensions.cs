using Ecommerce.Shared.Models;

namespace Ecommerce.DAL.Extensions
{
    public static class ProductMappingExtensions
    {
        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                ProductCategories = (product.ProductCategories ?? new List<ProductCategory>())
                    .ToDictionary(pc => pc.CategoryId, pc => pc.Category.Name)
            };
        }
    }
}
