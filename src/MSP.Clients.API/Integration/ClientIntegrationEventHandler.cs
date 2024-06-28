using MSP.Clients.API.Application.Services;
using MSP.Clients.API.DTOs;
using MSP.Core.Integration;
using MSP.Core.Models;
using MSP.MessageBus;
using MSP.WebAPI.Services;

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

    private void SetResponder()
    {
        _bus.RespondAsync<ClientRegisteredIntegrationEvent, MessageResponse>(async req => await CreateClientAsync(req));
        _bus.AdvancedBus.Connected += OnConnect;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponder();
        return Task.CompletedTask;
    }
    
    private void OnConnect(object s, EventArgs e)
    {
        SetResponder();
    }

    private async Task<MessageResponse> CreateClientAsync(ClientRegisteredIntegrationEvent message)
    {
        using var scope = _serviceProvider.CreateScope();
        var clientAppService = scope.ServiceProvider.GetRequiredService<IClientAppService>();
        var notification = scope.ServiceProvider.GetRequiredService<INotificationCollector>();

        await clientAppService.CreateAsync(new ClientRequestDTO
        {
            Email = message.Email,
            DocumentNumber = message.DocumentNumber,
            Name = message.Name
        });
        return new MessageResponse(notification.Notifications);
    }
}