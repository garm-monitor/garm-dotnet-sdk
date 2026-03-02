using Microsoft.Extensions.DependencyInjection;
using System;

namespace Garm.Sdk
{
    public static class GarmExtensions
    {
    public static IServiceCollection AddGarmMonitor(this IServiceCollection services, string token, Action<GarmOptions> configureOptions = null)
        {
            var options = new GarmOptions();
            configureOptions?.Invoke(options);

            // 1. Inicializa o Singleton estático do seu SDK
            GarmClient.Init(token, options.BaseUrl);

            // 2. Registra a instância no container de dependências para quem quiser usar via Injeção
            services.AddSingleton(GarmClient.Instance);

            return services;
        }
    }
}