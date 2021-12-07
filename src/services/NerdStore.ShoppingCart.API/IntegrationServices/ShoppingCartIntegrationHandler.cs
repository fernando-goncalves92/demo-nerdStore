using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NerdStore.Core.SharedEvents;
using NerdStore.MessageBus;
using NerdStore.ShoppingCart.API.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.ShoppingCart.API.IntegrationServices
{
    public class ShoppingCartIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public ShoppingCartIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();

            return Task.CompletedTask;
        }

        private void SetSubscribers()
        {
            _bus.Subscribe<OrderPlacedIntegrationEvent>("PedidoRealizado", async message => await RemoveShoppingCartFromDatabase(message));
        }

        private async Task RemoveShoppingCartFromDatabase(OrderPlacedIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ShoppingContext>();
                var shoppingCart = await context
                    .ShoppingCarts
                    .FirstOrDefaultAsync(c => c.CustomerId == message.CustomerId);

                if (shoppingCart != null)
                {
                    context.ShoppingCarts.Remove(shoppingCart);

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
