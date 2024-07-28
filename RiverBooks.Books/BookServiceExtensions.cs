using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace RiverBooks.Books;

public static class BookServiceExtensions {

    public static IServiceCollection AddBookModuleService(this IServiceCollection services
    ,ConfigurationManager configurationManager
    ,ILogger logger,
List<System.Reflection.Assembly> mediatRAssemblies)    
    {
        string? connectionString = configurationManager.GetConnectionString("BooksConnectionStrings");

        services.AddDbContext<BookDBContext>(options => 
            options.UseNpgsql(connectionString));

        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IBookRepository, BookRepository>();    

        logger.Information("{Module} module services registered","Books");

        mediatRAssemblies.Add(typeof(BookServiceExtensions).Assembly);


        return services;
    }
}
