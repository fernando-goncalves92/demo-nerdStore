using Microsoft.Extensions.DependencyInjection;
using NerdStore.ShoppingCart.API.IntegrationServices;

namespace NerdStore.ShoppingCart.API.Configurations
{
    public static class BackgroundServiceConfig
    {
        public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<ShoppingCartIntegrationHandler>();
            
            return services;
        }
    }
}
