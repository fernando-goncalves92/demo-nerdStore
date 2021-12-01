using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Core.Mediator;
using NerdStore.Order.API.Application.Queries.Voucher;
using NerdStore.Order.Domain.Voucher;
using NerdStore.Order.Infra.Data;
using NerdStore.WebAPI.Core.Facilities;

namespace NerdStore.Order.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("NerdStoreConnection"));
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<OrderDbContext>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IVoucherQuery, VoucherQuery>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();

            return services;
        }
    }
}