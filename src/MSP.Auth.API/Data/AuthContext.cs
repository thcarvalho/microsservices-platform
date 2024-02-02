using Microsoft.EntityFrameworkCore;
using MSP.Auth.API.Models;
using MSP.Core.Models;

namespace MSP.Auth.API.Data;

public class AuthContext : DbContext
{
    public DbSet<AuthUser> AuthUsers { get; set; }
    public DbSet<AuthUserRole> AuthUserRoles { get; set; }

    public AuthContext(DbContextOptions<AuthContext> opts) : base(opts)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(9, 2);
        configurationBuilder.Properties<string>().HaveColumnType("varchar(100)");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<AuthUserRole>()
            .HasData(Enumeration<AuthUserRole>.GetAll());

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}