namespace RiverBooks.Books;

internal interface IReadonlyBookRepository {

    Task<Book?> GetByIdAsync (Guid id);

    Task<List<Book>> ListAsync ();
}
