using MyBud.ProductsApi.Models.Core;

namespace MyBud.ProductsApi.Interfaces
{
    public interface IProductsService
    {
        Task<Product?> GetProductById(int productId);
        Task<IQueryable<Product>> GetProducts();
        Task<Product> CreateProduct(Product product);
        Task<bool> DeleteProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> UpdateProductPrice(Product product);
        Task<IEnumerable<Product>> SearchProducts(string value);
    }
}
