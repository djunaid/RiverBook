
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books;

internal class BookRepository : IBookRepository
{
    private readonly BookDBContext _dbContext;
    public BookRepository(BookDBContext bookDBContext)
    {
        _dbContext = bookDBContext;
    }
    public Task AddBookAsync(Book book)
    {
        _dbContext.Add(book);
        return Task.CompletedTask;
    }

    public Task DeleteBookAsync(Book book)
    {
        _dbContext.Remove(book);
        return Task.CompletedTask;
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Books.FindAsync(id);
    }

    public async Task<List<Book>> ListAsync()
    {
        return await _dbContext.Books.ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePriceAsync(Book book, decimal price)
    {
        var updateBook = await _dbContext.Books.FindAsync(book.Id);
        updateBook.UpdatePrice(price);

    }
}