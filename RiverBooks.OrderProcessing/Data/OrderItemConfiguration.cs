using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiverBooks.OrderProcessing.Data
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(o => o.Id)
                .ValueGeneratedNever();

            builder.Property(o => o.Description)
                .HasMaxLength(100)
                .IsRequired();


        }
    }
}
