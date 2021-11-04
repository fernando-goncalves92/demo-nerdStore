using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Core.Extensions;
using System;

namespace NerdStore.MessageBus
{
    public static class RabbitMQMessageBusConfiguration 
    {
        public static IServiceCollection AddRabbitMQMessageBusConfig(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetMessageQueueConnection("RabbitMQ");

            if (string.IsNullOrEmpty(connection))
            {
                throw new ArgumentNullException("Connection string deve ser informada no arquivo appsettings.json");
            }

            services.AddSingleton<IMessageBus>(new RabbitMQMessageBus(connection));

            return services;
        }
    }
}
