using Microsoft.OpenApi.Models;

namespace MyBud.ProductsApi.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Products API - V1", Version = "v1" });
            });
        }

        public static void UseConfiguredSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Products API - V1");
            });
        }
    }
}
