using System;
using Microsoft.Extensions.DependencyInjection;

namespace Garm.Sdk
{
    public static class GarmExtensions
    {
        // Esse método mágico estende o IServiceCollection
        public static IServiceCollection AddGarm(this IServiceCollection services, Action<GarmOptions> configure)
        {
            // 1. Cria as opções e roda a configuração que o usuário passou
            var options = new GarmOptions();
            configure(options);

            // 2. Registra as opções para quem precisar
            services.AddSingleton(options);

            // 3. Registra o GarmClient usando HttpClientFactory
            services.AddHttpClient<GarmClient>();

            return services;
        }
    }
}