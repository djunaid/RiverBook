using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace RiverBooks.OrderProcessing.Data
{
    internal class OrderDBContext : IdentityDbContext
    {


        public OrderDBContext(DbContextOptions<OrderDBContext> options) :
            base(options)
        { }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.HasDefaultSchema("OrderProcessing");

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>()
                .HavePrecision(18, 6);
        }


    }
}