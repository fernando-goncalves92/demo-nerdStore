using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.ShoppingCart.API.Data;
using NerdStore.WebAPI.Core.Facilities;

namespace NerdStore.ShoppingCart.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShoppingContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("NerdStoreConnection"));
            });

            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<ShoppingContext>();
            
            return services;
        }
    }
}
