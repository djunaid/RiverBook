namespace RiverBooks.Users.Contracts
{
    public record UserAddressDetails(Guid UserId, Guid AddressId, string Street1, string Street2, string City, string PostalCode, string State, string Country);
}
