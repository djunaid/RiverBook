using Ardalis.GuardClauses;

namespace RiverBooks.Users.Domain;


public class UserStreetAddress
{
    public UserStreetAddress() { }

    public UserStreetAddress(Guid userId, Address address)
    {
        UserId = Guard.Against.NullOrEmpty(userId);
        StreetAddress = Guard.Against.Null(address);
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UserId { get; private set; }

    public Address StreetAddress { get; private set; } = default!;
}

