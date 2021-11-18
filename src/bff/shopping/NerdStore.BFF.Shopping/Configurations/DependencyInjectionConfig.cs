using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.BFF.Shopping.DelegatingHandlers;
using NerdStore.BFF.Shopping.Services.Catalog;
using NerdStore.BFF.Shopping.Services.ShoppingCart;
using NerdStore.WebAPI.Core.Facilities;
using NerdStore.WebAPI.Core.Polly;

namespace NerdStore.BFF.Shopping.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyConfig.WaitAndRetryConfig())
                .AddTransientHttpErrorPolicy(PollyConfig.CircuitBreakerConfig);

            services.AddHttpClient<IShoppingCartService, ShoppingCartService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyConfig.WaitAndRetryConfig())
                .AddTransientHttpErrorPolicy(PollyConfig.CircuitBreakerConfig);

            return services;
        }
    }
}
