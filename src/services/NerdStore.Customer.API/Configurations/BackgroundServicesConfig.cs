using Microsoft.Extensions.DependencyInjection;
using NerdStore.Customer.API.IntegrationServices;

namespace NerdStore.Customer.API.Configurations
{
    public static class BackgroundServicesConfig
    {
        public static IServiceCollection AddBackgroundServicesConfig(this IServiceCollection services)
        {
            services.AddHostedService<CreateCustomerIntegrationHandler>();

            return services;
        }
    }
}
