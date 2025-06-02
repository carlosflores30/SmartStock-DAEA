using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smartstock.Application.Interfaces;
using Smartstock.Application.Services;
using Smartstock.Domain.Interfaces;
using Smartstock.Infrastructure.Data;
using Smartstock.Infrastructure.Mappings;
using Smartstock.Infrastructure.Repositories;

namespace Smartstock.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SmartstockDbContext>((serviceProvider, options) =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString);
        });

        services.AddAutoMapper(typeof(Smartstock.Infrastructure.Mappings.MappingProfile));
        services.AddAutoMapper(typeof(Smartstock.Infrastructure.Mappings.ProductProfile));
        services.AddAutoMapper(typeof(Smartstock.Infrastructure.Mappings.CategoryProfile));
        services.AddAutoMapper(typeof(Smartstock.Infrastructure.Mappings.CategoryInfraToDomainProfile));
        services.AddAutoMapper(typeof(Smartstock.Infrastructure.Mappings.ProductInfraToDomainProfile));
        // Registrar repositorios
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}