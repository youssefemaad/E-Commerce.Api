using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Data;
using Presistence.Data.Identity;
using Presistence.Repository;
using StackExchange.Redis;

namespace Presistence;

public static class InfraStractureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDataSeeding, DataSeeding>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBasketRepository,BasketRepository>();
        services.AddScoped<ICacheRepository, CacheRepository>();
        services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContex>();
        
        services.AddSingleton<IConnectionMultiplexer>((_) => 
        {
            return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnectionString"));
        });

        services.AddDbContext<StoreDbContext>(Opt =>
        {
            Opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddDbContext<StoreIdentityDbContex>(Opt =>
        {
            Opt.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
        });

        return services;
    }
}
