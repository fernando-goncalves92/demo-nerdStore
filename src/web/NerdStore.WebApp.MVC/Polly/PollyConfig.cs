using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;

namespace NerdStore.WebApp.MVC.Polly
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
                }, (outcome, timespan, retryCount, context) =>
                {
                    // Este método é executado antes da nova tentativa

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Tentando pela {retryCount} vez!");
                    Console.ForegroundColor = ConsoleColor.White;
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
