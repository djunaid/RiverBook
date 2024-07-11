namespace RiverBooks.Books;

internal interface IBookRepository : IReadonlyBookRepository
{

    Task AddBookAsync (Book book);

    Task DeleteBookAsync(Book book);

    Task SaveChangesAsync();

    Task UpdatePriceAsync(Book book, decimal price);

}
