using MSP.Core.Models;

namespace MSP.Clients.API.Models;

public class Client : Entity
{
    public string Name { get; protected set; }
    public string DocumentNumber { get; protected set; }
    public string Email { get; protected set; }

    protected Client() { }

    public Client(string name, string documentNumber, string email)
    {
        Name = name;
        DocumentNumber = documentNumber;
        Email = email;
    }

    public void UpdateClient(string name, string documentNumber, string email)
    {
        Name = name;
        DocumentNumber = documentNumber;
        Email = email;
    }
}