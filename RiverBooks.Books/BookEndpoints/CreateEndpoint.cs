using FastEndpoints;

namespace RiverBooks.Books;
internal class Create(IBookService bookService) : Endpoint<CreateRequest, BookDTO>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Post("/book");
        AllowAnonymous();

    }

    public override async Task HandleAsync(CreateRequest req, CancellationToken ct)
    {
        var book = new BookDTO(req.Id ?? Guid.NewGuid(), req.Title, req.Author, req.Price);

        await _bookService.CreateBookAsync(book);

        await SendCreatedAtAsync<GetByIdEndpoint>(new { book.Id}, book);
    }
}
