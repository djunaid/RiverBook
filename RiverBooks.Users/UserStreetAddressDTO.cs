namespace RiverBooks.Users
{
    public record UserStreetAddressDTO(Guid Id, Guid UserId, string Street1, string Street2, string City, string State, string PostalCode, string Country);

}
