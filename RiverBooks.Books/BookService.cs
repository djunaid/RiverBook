

namespace RiverBooks.Books;

internal class BookService : IBookService{

    public List<BookDTO> ListBooks(){
        return [
            new BookDTO (Guid.NewGuid(), "Star Wars Chapter I", "J. J. Thompson"),
            new BookDTO (Guid.NewGuid(), "Star Wars Chapter II", "R. Samson"),
            new BookDTO (Guid.NewGuid(), "Legend of Zelda", "Drew Ang")
        ];
    }


    
}
