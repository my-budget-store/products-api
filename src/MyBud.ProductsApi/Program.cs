using MyBud.ProductsApi.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        var enumConverter = new JsonStringEnumConverter();
        opts.JsonSerializerOptions.Converters.Add(enumConverter);
    });

builder.Services
    .ConfigureDatabase(builder.Configuration)
    .ConfigureApiVersioningAndExplorer()
    .ConfigureSwagger()
    .ConfigureInjectedServices();

var app = builder.Build();

app.UseConfiguredDatabase();
if (app.Environment.IsDevelopment())
{
    app.UseConfiguredSwagger();
}

app.UseHttpsRedirection()
   .UseAuthorization();

app.MapControllers();
app.Run();
