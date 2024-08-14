using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Infrastructure.Data;

public class UserStreetAddressConfiguration : IEntityTypeConfiguration<UserStreetAddress>
{
    public void Configure(EntityTypeBuilder<UserStreetAddress> builder)
    {
        builder.ToTable("UserStreetAddress");

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.ComplexProperty(x => x.StreetAddress);

    }
}


