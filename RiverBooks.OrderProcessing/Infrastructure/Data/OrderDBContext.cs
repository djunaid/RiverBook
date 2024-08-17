using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.SharedKernel;
using System.Reflection;

namespace RiverBooks.OrderProcessing.Infrastructure.Data
{
    internal class OrderDBContext : IdentityDbContext
    {
        private readonly IDomainEventDispatcher? _dispatcher;

        public OrderDBContext(DbContextOptions<OrderDBContext> options, IDomainEventDispatcher? domainEventDispatcher) :
            base(options)
        {
            _dispatcher = domainEventDispatcher;
        }

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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = await base.SaveChangesAsync(cancellationToken);

            if (_dispatcher is null)
            {
                return result;
            }

            var entitiesWithEvents = ChangeTracker.Entries<IHaveDomainEvents>()
                .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any())
            .ToArray();

            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

            return result;
        }


    }
}