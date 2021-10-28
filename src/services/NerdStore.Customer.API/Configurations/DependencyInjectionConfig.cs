using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Customer.API.Data;

namespace NerdStore.Customer.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomerDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("NerdStoreConnection"));
            });

            services.AddScoped<CustomerDbContext>();

            return services;
        }
    }
}
