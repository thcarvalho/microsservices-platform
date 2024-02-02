using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MSP.Clients.API.Data;

public class DbContextFactory : IDesignTimeDbContextFactory<ClientsContext>
{
    public ClientsContext CreateDbContext(string[] args)
    {
        var configuration = Core.Utils.Configuration.GetConfiguration("MSP.Clients.API");

        var builder = new DbContextOptionsBuilder<ClientsContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseSqlServer(connectionString);
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development"))
        {
            builder.EnableSensitiveDataLogging();
            builder.EnableDetailedErrors();
        }
        return new ClientsContext(builder.Options);
    }
}