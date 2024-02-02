using Microsoft.EntityFrameworkCore;
using MSP.Clients.API.Models;
using MSP.Data.Core;

namespace MSP.Clients.API.Data.Repositories;

public class ClientRepository : BaseRepository<Client>, IClientRepository
{
    public ClientRepository(ClientsContext context) : base(context)
    {
    }
}