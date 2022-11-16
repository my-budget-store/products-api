using MyBud.ProductsApi.Interfaces;
using MyBud.ProductsApi.Models.Core;

namespace MyBud.ProductsApi.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<Product?> GetProductById(int productId)
        {
            return await _productsRepository.GetProductById(productId);
        }

        public async Task<IQueryable<Product>> GetProducts()
        {
            return await _productsRepository.GetProducts();
        }

        public async Task<IEnumerable<Product>> SearchProducts(string value)
        {
            return await _productsRepository.SearchProducts(value);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            return await _productsRepository.CreateProduct(product);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            return await _productsRepository.UpdateProduct(product);
        }

        public async Task<Product> UpdateProductPrice(Product product)
        {
            return await _productsRepository.UpdateProductPrice(product);
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            return await _productsRepository.DeleteProduct(product);
        }
    }
}
