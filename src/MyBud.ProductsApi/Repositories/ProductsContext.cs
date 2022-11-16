using Microsoft.EntityFrameworkCore;
using MyBud.ProductsApi.Models.Core;

namespace MyBud.ProductsApi.Repositories
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Product>()
                .Property(e => e.Category)
                .HasConversion<string>();
        }

        public DbSet<Product> Products { get; set; }
    }
}
