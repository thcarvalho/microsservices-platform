using MSP.Core.CQRS;

namespace MSP.Core.Integration;

public class ClientRegisteredIntegrationEvent : IntegrationEvent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string DocumentNumber { get; set; }
    public int RoleId { get; set; }
}