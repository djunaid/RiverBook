namespace RiverBooks.Users;

public record AddCartItemCommand (string EmailAddress, Guid BookId, int Quantity);
