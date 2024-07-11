namespace RiverBooks.Books;

internal interface IBookService{
    Task<List<BookDTO>> ListBooksAsync();

    Task<BookDTO?> GetBookByIdAsync(Guid id);

    Task CreateBookAsync(BookDTO newBook);

    Task DeleteBookAsync(Guid id);

    Task UpdatePriceAsync(Guid bookId, decimal price);

    
}
