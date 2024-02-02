using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MSP.Auth.API.Data;

public class DbContextFactory : IDesignTimeDbContextFactory<AuthContext>
{
    public AuthContext CreateDbContext(string[] args)
    {
        var configuration = Core.Utils.Configuration.GetConfiguration("MSP.Auth.API");

        var builder = new DbContextOptionsBuilder<AuthContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseSqlServer(connectionString);
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development"))
        {
            builder.EnableSensitiveDataLogging();
            builder.EnableDetailedErrors();
        }
        return new AuthContext(builder.Options);
    }
}