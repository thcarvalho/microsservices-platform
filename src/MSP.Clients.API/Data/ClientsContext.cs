using Microsoft.EntityFrameworkCore;
using MSP.Clients.API.Models;

namespace MSP.Clients.API.Data;

public class ClientsContext : DbContext
{
    public DbSet<Client> Clients { get; set; }

    public ClientsContext(DbContextOptions<ClientsContext> opts) : base(opts)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(9, 2);
        configurationBuilder.Properties<string>().HaveColumnType("varchar(100)");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientsContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}