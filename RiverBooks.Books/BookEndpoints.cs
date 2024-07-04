using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace RiverBooks.Books;

public static class BookEndpoints
{
    public static void MapBookEndPoints(this WebApplication app)
    {

        app.MapGet("/books", (IBookService bookService) => bookService.ListBooks());
    }


}

public class ListBookResponse
{
    public List<BookDTO> Books { get; set; }
}

internal class ListBooksEndpoint(IBookService bookService) : EndpointWithoutRequest<ListBookResponse>
{

    private IBookService _bookService = bookService;

    public override void Configure()
    {
        Get("/api/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {

        var books = _bookService.ListBooks();

        await SendAsync(new ListBookResponse
        {
            Books = books
        });
    }

}
