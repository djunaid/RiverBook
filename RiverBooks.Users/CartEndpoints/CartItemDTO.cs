namespace RiverBooks.Users.CartEndpoints
{
    public record CartItemDTO(Guid Id, Guid BookId, string Description, int Quantity, decimal UnitPrice);

}
