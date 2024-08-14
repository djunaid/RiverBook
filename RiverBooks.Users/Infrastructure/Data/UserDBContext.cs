using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RiverBooks.SharedKernel;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.Infrastructure.Data;

public class UserDBContext : IdentityDbContext
{
    private readonly IDomainEventDispatcher? _dispatcher;

    public UserDBContext(DbContextOptions<UserDBContext> options, IDomainEventDispatcher? dispatcher) :
        base(options)
    {
        _dispatcher = dispatcher;
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<UserStreetAddress> UserStreetAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.HasDefaultSchema("Users");

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
