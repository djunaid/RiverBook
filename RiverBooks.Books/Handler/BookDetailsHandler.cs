using Ardalis.Result;
using MediatR;
using RiverBooks.Books.Contracts;

namespace RiverBooks.Books.Contracts
{
    internal class BookDetailsHandler : IRequestHandler<BookDetailsQuery, Result<BookDetailsResponse>>
    {
        public BookDetailsHandler(IBookService bookService)
        {
            _bookService = bookService;           
        }

        private readonly IBookService _bookService;
       

        public async Task<Result<BookDetailsResponse>> Handle(BookDetailsQuery request, CancellationToken cancellationToken)
        {
            var book  = await _bookService.GetBookByIdAsync(request.BookId);

            if (book == null)
            {
                return Result.NotFound();
            }

            return Result.Success(new BookDetailsResponse(book.Id, book.Title, book.Author, book.Price));
        }
    }
}
