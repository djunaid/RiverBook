using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiverBooks.OrderProcessing.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.OrderProcessing.Infrastructure.Data
{
    internal static class Constants
    {
        internal static int STREET1_MAXLENGTH = 50;
        internal static int STREET2_MAXLENGTH = 50;
        internal static int CITY_MAXLENGTH = 25;
        internal static int STATE_MAXLENGTH = 25;
        internal static int POSTAL_CODE_MAXLENGTH = 10;
        internal static int COUNTRY_MAXLENGTH = 20;
    }

    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entityTypeBuilder)
        {

            entityTypeBuilder.Property(o => o.Id).ValueGeneratedNever();

            entityTypeBuilder.ComplexProperty(o => o.ShippingAddress, address =>
            {
                address.Property(a => a.Street1).HasMaxLength(Constants.STREET1_MAXLENGTH);

                address.Property(a => a.Street2).HasMaxLength(Constants.STREET2_MAXLENGTH);

                address.Property(a => a.City).HasMaxLength(Constants.CITY_MAXLENGTH);

                address.Property(a => a.PostalCode).HasMaxLength(Constants.POSTAL_CODE_MAXLENGTH);

                address.Property(a => a.Country).HasMaxLength(Constants.COUNTRY_MAXLENGTH);

            });


            entityTypeBuilder.ComplexProperty(o => o.BillingAddress, address =>
            {
                address.Property(a => a.Street1).HasMaxLength(Constants.STREET1_MAXLENGTH);

                address.Property(a => a.Street2).HasMaxLength(Constants.STREET2_MAXLENGTH);

                address.Property(a => a.City).HasMaxLength(Constants.CITY_MAXLENGTH);

                address.Property(a => a.PostalCode).HasMaxLength(Constants.POSTAL_CODE_MAXLENGTH);

                address.Property(a => a.Country).HasMaxLength(Constants.COUNTRY_MAXLENGTH);

            });
        }
    }
}
