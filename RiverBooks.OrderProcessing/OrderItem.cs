using Ardalis.GuardClauses;

namespace RiverBooks.OrderProcessing
{
    internal class OrderItem
    {

        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid BookId { get; private set; }

        public string Description { get; private set; } = string.Empty;

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; private set; }

        public OrderItem(Guid bookId, string description, int quantity, decimal unitPrice)
        {
            BookId = Guard.Against.Default(bookId);
            Description = Guard.Against.NullOrEmpty(description);
            Quantity = Guard.Against.NegativeOrZero(quantity);
            UnitPrice = Guard.Against.Negative(unitPrice);
        }

    }
}