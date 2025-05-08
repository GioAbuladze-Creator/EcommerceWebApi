using Ecommerce.Shared.Commands;
using Ecommerce.Shared.Models;

namespace Ecommerce.Shared.Abstractions
{
    public interface IProductsService
    {
        Task<List<ProductDto>> GetProductsAsync();
        Task<Product> GetProductAsync(int productId);
        Task<ProductDto> GetProductDtoAsync(int productId);
        Task<int> CreateProductAsync(CreateProductCommandDto product);
        Task DeleteProductAsync(int productId);
        Task UpdateProductAsync(UpdateProductCommandDto productDto);
    }
}
