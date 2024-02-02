using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSP.Core.Models;

namespace MSP.Data.Core;

public class BaseEnumerationMapping<TEnumeration> : IEntityTypeConfiguration<TEnumeration> where TEnumeration : Enumeration<TEnumeration>
{
    public void Configure(EntityTypeBuilder<TEnumeration> builder)
    {
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name).IsRequired();
    }
}