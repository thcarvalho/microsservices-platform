using MSP.Core.CQRS;

namespace MSP.Clients.API.Application.Events;

public class ClientRegisteredEvent : Event
{
    public int Id { get; set; }
    public int AuthUserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string DocumentNumber { get; set; }
}