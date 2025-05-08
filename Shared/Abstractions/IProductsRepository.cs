using Ecommerce.Shared.Commands;
using Ecommerce.Shared.Models;

namespace Ecommerce.Shared.Abstractions
{
    public interface IProductsRepository
    {
        Task<List<ProductDto>> GetProductsAsync();
        Task<Product> GetProductAsync(int productId);
        Task<ProductDto> GetProductDtoAsync(int productId);
        Task<int> CreateProductAsync(CreateProductCommandDto productDto);
        Task DeleteProductAsync(int id);
        Task UpdateProductAsync(UpdateProductCommandDto productDto);
    }
}
