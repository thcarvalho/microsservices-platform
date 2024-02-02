using MSP.Data.Core;

namespace MSP.Clients.API.Data;

public class ClientsUnitOfWork : UnitOfWork<ClientsContext>, IClientsUnitOfWork
{
    public ClientsUnitOfWork(ClientsContext context) : base(context)
    {
    }
}