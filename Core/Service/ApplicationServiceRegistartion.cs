using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;

namespace Service
{
    public static class ApplicationServiceRegistartion
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AssemblyReference).Assembly);
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IServiceManager, ServiceMangerWithDelegates>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<Func<IProductService>>(provider => () => provider.GetRequiredService<IProductService>());

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<Func<IBasketService>>(provider => () => provider.GetRequiredService<IBasketService>());

            services.AddScoped<IOrderSerivce, OrderSerivce>();
            services.AddScoped<Func<IOrderSerivce>>(provider => () => provider.GetRequiredService<IOrderSerivce>());

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<Func<IAuthenticationService>>(provider => () => provider.GetRequiredService<IAuthenticationService>());

            
            
            return services;
        }
    }
}
