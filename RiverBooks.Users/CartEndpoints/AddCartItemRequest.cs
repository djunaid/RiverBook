namespace RiverBooks.Users;

public record AddCartItemRequest (Guid BookId, int Quantity);
