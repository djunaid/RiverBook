using FastEndpoints;

namespace RiverBooks.Books;

internal class UpdatePriceEndpoint(IBookService bookService) : Endpoint<UpdatePriceRequest, BookDTO> 
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Post("/books/{Id}/pricehistory");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdatePriceRequest req, CancellationToken ct)
    {
        await _bookService.UpdatePriceAsync(req.Id, req.Price);

        var updatedBook = await _bookService.GetBookByIdAsync(req.Id);

        await SendAsync(updatedBook);
    }
}
