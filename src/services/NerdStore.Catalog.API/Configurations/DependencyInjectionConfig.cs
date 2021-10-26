using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalog.API.Data;

namespace NerdStore.Catalog.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("NerdStoreConnection"));
            });

            services.AddScoped<CatalogDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
