using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books;

public class BookDBContext : DbContext
{

    internal DbSet<Book> Books { get; set; }

    public BookDBContext(DbContextOptions<BookDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Books");

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(18,6);
    }
}
