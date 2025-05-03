using Ecommerce.Shared.Commands;
using Ecommerce.Shared.Models;

namespace Ecommerce.Shared.Abstractions
{
    public interface IProductsService
    {
        Task<List<ProductFullInfo>> GetProductsAsync();
        Task<ProductFullInfo> GetProductAsync(int productId);
        Task<int> CreateProductAsync(CreateProductCommandDto product);
        Task DeleteProductAsync(int productId);
        Task UpdateProductAsync(UpdateProductCommandDto productDto);

    }
}
