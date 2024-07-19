using FastEndpoints;

namespace RiverBooks.Books;

internal class ListEndpoint(IBookService bookService) : EndpointWithoutRequest<ListResponse>
{

    private IBookService _bookService = bookService;

    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {

        var books = await _bookService.ListBooksAsync();

        await SendAsync(new ListResponse
        {
            Books = books
        });
    }

}
