using Ecommerce.Shared.Abstractions;
using Ecommerce.Shared.Commands;
using Ecommerce.Shared.Models;

namespace Ecommerce.BLL
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var products = await _productsRepository.GetProductsAsync();
            return products;
        }
        public async Task<Product> GetProductAsync(int productId)
        {
            var product = await _productsRepository.GetProductAsync(productId);
            return product;
        }
        public async Task<ProductDto> GetProductDtoAsync(int productId)
        {
            var product = await _productsRepository.GetProductDtoAsync(productId);
            return product;
        }
        public async Task<int> CreateProductAsync(CreateProductCommandDto product)
        {
            var productId = await _productsRepository.CreateProductAsync(product);
            return productId;
        }
        public async Task DeleteProductAsync(int productId)
        {
            await _productsRepository.DeleteProductAsync(productId);
        }
        public async Task UpdateProductAsync(UpdateProductCommandDto productDto)
        {
            await _productsRepository.UpdateProductAsync(productDto);
        }


    }
}
