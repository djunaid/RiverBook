using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Users.Data;
using Serilog;

namespace RiverBooks.Users;

public static class UserServiceExtensions
{

    public static IServiceCollection AddUserServices(this IServiceCollection services,
        ConfigurationManager configurationManager
        , ILogger logger,
        List<Assembly> mediatRAssemblies)
    {

        var connectionString = configurationManager.GetConnectionString("UsersConnectionStrings");

        services.AddDbContext<UserDBContext>(config =>
                config.UseNpgsql(connectionString));

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<UserDBContext>();

        logger.Information("{Module} module has been registered", "Users");

        mediatRAssemblies.Add(typeof(UserServiceExtensions).Assembly);

        services.AddScoped<IApplicatinUserRepository,EfApplicationUserRepository>();


        return services;
    }

}
