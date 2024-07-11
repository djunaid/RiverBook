using FastEndpoints;

namespace RiverBooks.Books;

internal class DeleteEndpoint(IBookService bookService) : Endpoint<DeleteReqeust>{
    
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Delete("/books/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteReqeust req, CancellationToken ct)
    {
        await _bookService.DeleteBookAsync(req.Id);

        await SendNoContentAsync();
    }

}
