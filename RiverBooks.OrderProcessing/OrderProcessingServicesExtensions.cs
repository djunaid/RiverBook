using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.OrderProcessing.Data;
using Serilog;

namespace RiverBooks.OrderProcessing;

public static class OrderProcessingModuleServiceExtensions
{

    public static IServiceCollection AddOrderProcessingModuleServices(this IServiceCollection services,
        ConfigurationManager configurationManager
        , ILogger logger,
        List<Assembly> mediatRAssemblies)
    {

        var connectionString = configurationManager.GetConnectionString("OrdersConnectionStrings");

        services.AddDbContext<OrderDBContext>(config =>
                config.UseNpgsql(connectionString));
     
        logger.Information("{Module} module has been registered", "Order Processing");

        mediatRAssemblies.Add(typeof(OrderProcessingModuleServiceExtensions).Assembly);

        services.AddScoped<IOrderRepository,EfOrderRepository>();


        return services;
    }

}
