using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.WebAPI.Core.Facilities;
using NerdStore.WebAPI.Core.Polly;
using NerdStore.WebApp.MVC.DelegatingHandlers;
using NerdStore.WebApp.MVC.Services.Authentication;
using NerdStore.WebApp.MVC.Services.Catalog;
using NerdStore.WebApp.MVC.Services.Customer;
using NerdStore.WebApp.MVC.Services.ShoppingBff;

namespace NerdStore.WebApp.MVC.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthenticationService, AuthenticationService>()
                .AddPolicyHandler(PollyConfig.WaitAndRetryConfig())
                .AddTransientHttpErrorPolicy(PollyConfig.CircuitBreakerConfig);

            services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyConfig.WaitAndRetryConfig())
                .AddTransientHttpErrorPolicy(PollyConfig.CircuitBreakerConfig);

            services.AddHttpClient<IShoppingBffService, ShoppingBffService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyConfig.WaitAndRetryConfig())
                .AddTransientHttpErrorPolicy(PollyConfig.CircuitBreakerConfig);

            services.AddHttpClient<ICustomerService, CustomerService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyConfig.WaitAndRetryConfig())
                .AddTransientHttpErrorPolicy(PollyConfig.CircuitBreakerConfig);

            return services;
        }
    }
}
