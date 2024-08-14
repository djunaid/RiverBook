using Ardalis.GuardClauses;

namespace RiverBooks.Users.Domain;

public class CartItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid BookId { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public int Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public CartItem()
    {
        // EF
    }

    public CartItem(Guid bookId, string description, int quantity, decimal price)
    {
        BookId = Guard.Against.Default(bookId);
        Description = Guard.Against.NullOrEmpty(description);
        Quantity = Guard.Against.Negative(quantity);
        UnitPrice = Guard.Against.Negative(price);
    }

    internal void UpdateQuantity(int quantity)
    {
        Quantity = Guard.Against.Negative(quantity);
    }

    internal void UpdateDescription(string description)
    {
        Description = Guard.Against.NullOrEmpty(description);
    }

    internal void UpdateUnitPrice(decimal unitPrice)
    {
        UnitPrice = Guard.Against.Negative(unitPrice);
    }
}