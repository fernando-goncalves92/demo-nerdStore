using EasyNetQ;
using NerdStore.Core.Messages;
using Polly;
using RabbitMQ.Client.Exceptions;
using System;
using System.Threading.Tasks;

namespace NerdStore.MessageBus
{
    public class RabbitMQMessageBus : IMessageBus
    {
        private IBus _bus;
        private readonly string _connectionString;

        public bool IsConnected => _bus?.IsConnected ?? false;
        public IAdvancedBus AdvancedBus => _bus?.Advanced;

        public RabbitMQMessageBus(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Publish<T>(T message) where T : IntegrationEvent
        {
            Connect();

            _bus.Publish(message);
        }

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            Connect();

            await _bus.PublishAsync(message);
        }

        public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
        {
            Connect();

            _bus.Subscribe(subscriptionId, onMessage);
        }

        public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
        {
            Connect();

            _bus.SubscribeAsync(subscriptionId, onMessage);
        }

        public TResponse Request<TRequest, TResponse>(TRequest request) where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            Connect();

            return _bus.Request<TRequest, TResponse>(request);
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent where TResponse : ResponseMessage
        {
            Connect();

            return await _bus.RequestAsync<TRequest, TResponse>(request);
        }

        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TRequest : IntegrationEvent where TResponse : ResponseMessage
        {
            Connect();

            return _bus.Respond(responder);
        }

        public IDisposable RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent where TResponse : ResponseMessage
        {
            Connect();
            
            return _bus.RespondAsync(responder);
        }

        private void Connect()
        {
            if (IsConnected)
            {
                return;
            }

            var policy = Policy
                .Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() =>
            {
                _bus = RabbitHutch.CreateBus(_connectionString);

                AdvancedBus.Disconnected += (s, e) =>
                {
                    var policyDisconnect = Policy.Handle<EasyNetQException>()
                        .Or<BrokerUnreachableException>()
                        .RetryForever();

                    policyDisconnect.Execute(Connect);
                };
            });
        }

        public void Dispose()
        {
            _bus.Dispose();
        }
    }
}
