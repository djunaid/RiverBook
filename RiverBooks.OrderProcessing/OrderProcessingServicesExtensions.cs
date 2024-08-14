using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.OrderProcessing.Infrastructure;
using RiverBooks.OrderProcessing.Infrastructure.Data;
using RiverBooks.OrderProcessing.Interface;
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
        services.AddScoped<RedisOrderAddressCache>();
        services.AddScoped<IOrderAddressCache, ReadThroughOrderAddressCache>();


        return services;
    }

}
