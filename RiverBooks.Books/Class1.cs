using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace RiverBooks.Books;

internal interface IBookService{
    IEnumerable<BookDTO> ListBooks();
}

public record BookDTO (Guid Id, string Title, string Author );

internal class BookService : IBookService{

    public IEnumerable<BookDTO> ListBooks(){
        return [
            new BookDTO (Guid.NewGuid(), "Star Wars Chapter I", "J. J. Thompson"),
            new BookDTO (Guid.NewGuid(), "Star Wars Chapter II", "R. Samson"),
            new BookDTO (Guid.NewGuid(), "Legend of Zelda", "Drew Ang")
        ];
    }
}

public static class BookEndpoints
{
    public static void MapBookEndPoints(this WebApplication app){

        app.MapGet("/books", (IBookService bookService) => bookService.ListBooks() );
    }
}


public static class BookServiceExtensions {

    public static IServiceCollection AddBookService(this IServiceCollection services){
        services.AddScoped<IBookService, BookService>();

        return services;
    }
}