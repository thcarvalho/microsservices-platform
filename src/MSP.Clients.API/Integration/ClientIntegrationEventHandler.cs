using MSP.Clients.API.Application.Services;
using MSP.Core.Integration;
using MSP.Core.Models;
using MSP.MessageBus;

namespace MSP.Clients.API.Integration;

public class ClientIntegrationEventHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public ClientIntegrationEventHandler(IMessageBus bus, IServiceProvider serviceProvider)
    {
        _bus = bus;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _bus.RespondAsync<ClientRegisteredIntegrationEvent, MessageResponse>(CreateClientAsync);
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
        }
    }

    private async Task<MessageResponse> CreateClientAsync(ClientRegisteredIntegrationEvent message)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var clientAppService = scope.ServiceProvider.GetRequiredService<IClientAppService>();
            return await clientAppService.CreateAsync(message);
        }
    }
}