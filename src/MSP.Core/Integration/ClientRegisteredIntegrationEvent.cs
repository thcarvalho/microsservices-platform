using MSP.Core.CQRS;

namespace MSP.Core.Integration;

public class ClientRegisteredIntegrationEvent : IntegrationEvent
{
    public int AuthUserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string DocumentNumber { get; set; }

    public ClientRegisteredIntegrationEvent(int authUserId, string name, string email, string documentNumber)
    {
        AuthUserId = authUserId;
        Name = name;
        Email = email;
        DocumentNumber = documentNumber;
    }
}