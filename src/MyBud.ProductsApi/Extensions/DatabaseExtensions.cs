using Microsoft.EntityFrameworkCore;
using MyBud.ProductsApi.Repositories;

namespace MyBud.ProductsApi.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, ConfigurationManager configuration)
        {
            return services.AddDbContext<ProductsContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("ProductsContext")));
        }

        public static void UseConfiguredDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ProductsContext>();
                db.Database.Migrate();
            }
        }
    }
}
