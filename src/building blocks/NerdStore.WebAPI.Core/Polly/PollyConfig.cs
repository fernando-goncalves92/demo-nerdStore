using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;

namespace NerdStore.WebAPI.Core.Polly
{
    public static class PollyConfig
    {
        public static AsyncRetryPolicy<HttpResponseMessage> WaitAndRetryConfig()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                        TimeSpan.FromSeconds(1),  // Primeira tentativa após 1 segundo do erro
                        TimeSpan.FromSeconds(5),  // após 5 segundos
                        TimeSpan.FromSeconds(10), // após 10 segundos
                });
        }

        public static AsyncCircuitBreakerPolicy<HttpResponseMessage> CircuitBreakerConfig(PolicyBuilder<HttpResponseMessage> builder)
        {
            const int NumberOfConsecutiveAttempts = 5;
            const int SecondsToCloseCircuit = 30;

            return builder.CircuitBreakerAsync(NumberOfConsecutiveAttempts, TimeSpan.FromSeconds(SecondsToCloseCircuit));
        }
    }
}
