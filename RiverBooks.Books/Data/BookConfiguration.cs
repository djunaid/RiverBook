using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiverBooks.Books;

public partial class BookConfiguration : IEntityTypeConfiguration<Book>
{
    private static readonly Guid Book1Id = new Guid("E2C8ECA2-9382-4FBC-88EA-D5A09CEA4E94");
    private static readonly Guid Book2Id = new Guid("2219FAC9-7FD0-46D9-9944-511660A9B764");
    private static readonly Guid Book3Id = new Guid("CD2B6E67-82D0-45DE-AFFF-DAB028F1E8F0");
    public void Configure(EntityTypeBuilder<Book> builder){
        
        builder.Property(x=> x.Title)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.Property(x=> x.Author)
        .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
        .IsRequired();

        builder.HasData(GetEnumerableBookData());

    }

    private IEnumerable<Book> GetEnumerableBookData(){
        var tolkien = "J.R.R. Tolkien";

        yield return new Book(Book1Id, "The Fellowship of the Ring", tolkien, 10.99m);
        yield return new Book(Book2Id, "The Two Towers", tolkien, 8.5m);
        yield return new Book(Book3Id, "The Return of the King", tolkien, 11.99m);

    }
}
