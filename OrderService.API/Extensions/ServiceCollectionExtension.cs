using OrderService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.HttpAdapter.Contract;
using OrderService.Infrastructure.HttpAdapter.Implementation;
using OrderService.Application.Contract;
using OrderApplication = OrderService.Application.Implementation.OrderService;
using OrderService.Domain.RepositoryContracts;
using OrderService.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OrderService.SharedKernel.ConfigurationModels;
using IAuthentication = OrderService.Application.Contract.IAuthenticationService;
using Authenticaton = OrderService.Application.Implementation.AuthenticationService;
using OrderService.Infrastructure.TokenGenerator;

namespace OrderService.API.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x => { x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; })
            .AddJwtBearer(opt => opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration.GetSection("JwtSettings:Issuer").Value,
                ValidAudience = configuration.GetSection("JwtSettings:Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:SecretKey").Value))
            });
        services.AddAuthorization();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ApplicationDb")));

        services.Configure<ProductPLatformSettings>(configuration.GetSection("ProductPLatformSettings"));

        services.AddHttpClient();
        services.AddScoped<IProductPlatform, ProductPlatform>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderApplication>();
        services.AddScoped<IAuthentication, Authenticaton>();
    }
}
