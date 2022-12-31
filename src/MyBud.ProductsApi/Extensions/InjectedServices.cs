using MyBud.ProductsApi.Interfaces;
using MyBud.ProductsApi.Repositories;

namespace MyBud.ProductsApi.Extensions
{
    public static class InjectedService
    {
        public static IServiceCollection ConfigureInjectedServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IProductsRepository, ProductsRepository>();
        }
    }
}
