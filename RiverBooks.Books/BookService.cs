


using System.Security;

namespace RiverBooks.Books;

internal class BookService : IBookService
{

    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task CreateBookAsync(BookDTO newBook)
    {
        var book = new Book(newBook.Id, newBook.Title, newBook.Author, newBook.Price);
        await _bookRepository.AddBookAsync(book);
        await _bookRepository.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book is not null)
        {

            await _bookRepository.DeleteBookAsync(book);
            await _bookRepository.SaveChangesAsync();

        }
    }

    public async Task<BookDTO?> GetBookByIdAsync(Guid id)
    {
        var book = (await _bookRepository.GetByIdAsync(id));

        if (book is not null)
            return new BookDTO(book.Id, book.Title, book.Author, book.Price);

    return null;
    }

    public async Task<List<BookDTO>> ListBooksAsync()
    {

        var books = (await _bookRepository.ListAsync())
                     .Select(book => new BookDTO(book.Id, book.Title, book.Author, book.Price)).ToList();

        return books;
    }

    public async Task UpdatePriceAsync(Guid bookId, decimal price)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);

        await _bookRepository.UpdatePriceAsync(book, price);
        await _bookRepository.SaveChangesAsync();
    }
}
