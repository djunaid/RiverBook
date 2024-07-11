using FastEndpoints;

namespace RiverBooks.Books;

internal class GetByIdEndpoint(IBookService bookService) : Endpoint<GetByIdRequest, BookDTO>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Get("/books/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetByIdRequest req, CancellationToken ct)
    {
        var book = await _bookService.GetBookByIdAsync(req.Id);

        if(book is not null){
            await SendAsync(book);
            return;
        }

        await SendNotFoundAsync();

    }

}
