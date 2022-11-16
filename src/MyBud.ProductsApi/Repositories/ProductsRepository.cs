using MyBud.ProductsApi.Interfaces;
using MyBud.ProductsApi.Models.Core;

namespace MyBud.ProductsApi.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductsContext _context;

        public ProductsRepository(ProductsContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetProductById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            return product;
        }

        public Task<IQueryable<Product>> GetProducts()
        {
            var products = _context.Products.AsQueryable();

            return Task.FromResult(products);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _context.AddAsync(product);
            _context.SaveChanges();

            return product;
        }

        public Task<IEnumerable<Product>> SearchProducts(string value)
        {
            var products = _context.Products.Where(p => p.Name.Contains(value)).AsEnumerable();

            return Task.FromResult(products);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.ProductId);

            if (existingProduct != null)
                _context.Update(existingProduct);

            await _context.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<Product> UpdateProductPrice(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.ProductId);

            if (existingProduct != null)
            {
                existingProduct.Price = product.Price;
                existingProduct.SalePrice = product.SalePrice;
                _context.Update(existingProduct);
            }

            await _context.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            var productState = _context.Remove(product);
            await _context.SaveChangesAsync();

            if (productState.State.Equals(Microsoft.EntityFrameworkCore.EntityState.Deleted))
            {
                return true;
            }

            return false;
        }
    }
}
