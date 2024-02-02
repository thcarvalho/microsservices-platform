using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSP.Auth.API.Models;
using MSP.Data.Core;

namespace MSP.Auth.API.Data.Mappings;

public class AuthUserMapping : BaseEntityMapping<AuthUser>
{
    protected override void ConfigureEntity(EntityTypeBuilder<AuthUser> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.PasswordHash).IsRequired();

        builder
            .HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.AuthUserRoleId);
    }
}