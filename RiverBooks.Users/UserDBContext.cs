using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Users;

public class UserDBContext : IdentityDbContext {


    public UserDBContext (DbContextOptions<UserDBContext> options) : 
        base(options){ }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }   

    protected override void OnModelCreating (ModelBuilder builder){

        builder.HasDefaultSchema("Users");

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  

        base.OnModelCreating(builder);

    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(18,6);
    }


}
