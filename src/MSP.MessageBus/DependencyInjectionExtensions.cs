using System;
using Microsoft.Extensions.DependencyInjection;

namespace MSP.MessageBus;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection ConfigureMessageBus(this IServiceCollection services, string connection)
    {
        if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();

        services.AddSingleton<IMessageBus>(new MessageBus(connection));

        return services;
    }
}