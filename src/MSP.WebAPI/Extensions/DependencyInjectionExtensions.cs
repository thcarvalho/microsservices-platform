using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSP.WebAPI.Services;

namespace MSP.WebAPI.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection ConfigureCoreServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationCollector, NotificationCollector>();
        return services;
    }
}