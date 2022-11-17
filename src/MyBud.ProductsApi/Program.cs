using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyBud.ProductsApi.Extensions;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        var enumConverter = new JsonStringEnumConverter();
        opts.JsonSerializerOptions.Converters.Add(enumConverter);
    });

builder.Services
    .ConfigureDatabase(config)
    .ConfigureApiVersioningAndExplorer()
    .ConfigureSwagger()
    .ConfigureInjectedServices();

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["Jwt:SecretKey"])),
    ValidateIssuer = true,
    ValidIssuer = config["Jwt:Issuer"],
    ValidateAudience = true,
    ValidAudience = config["Jwt:Audience"],
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero,
    RequireExpirationTime = true,
};

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.TokenValidationParameters = tokenValidationParameters;
    }); 

var app = builder.Build();

app.UseConfiguredDatabase();
if (app.Environment.IsDevelopment())
{
    app.UseConfiguredSwagger();
}

app.UseHttpsRedirection()
   .UseAuthentication()
   .UseAuthorization();

app.MapControllers();
app.Run();
