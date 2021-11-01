using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Core.Mediator;
using NerdStore.Customer.API.Commands;
using NerdStore.Customer.API.Data;
using NerdStore.Customer.API.Events;

namespace NerdStore.Customer.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            //services.AddScoped<IAspNetUser, AspNetUser>();
            
            services.AddScoped<CustomerDbContext>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<AddCustomerCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<AddCustomerAddressCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<INotificationHandler<AddedCustomerEvent>, CustomerEventHandler>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddDbContext<CustomerDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("NerdStoreConnection"));
            });

            return services;
        }
    }
}
