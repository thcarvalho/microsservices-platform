using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSP.Clients.API.Models;
using MSP.Data.Core;

namespace MSP.Clients.API.Data.Mappings;

public class ClientMapping : BaseEntityMapping<Client>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Client> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.DocumentNumber).HasMaxLength(11).IsRequired();
    }
}