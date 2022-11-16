using MyBud.ProductsApi.Interfaces;
using MyBud.ProductsApi.Repositories;
using MyBud.ProductsApi.Services;

namespace MyBud.ProductsApi.Extensions
{
    public static class InjectedService
    {
        public static IServiceCollection ConfigureInjectedServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IProductsService, ProductsService>()
                .AddTransient<IProductsRepository, ProductsRepository>();
        }
    }
}
