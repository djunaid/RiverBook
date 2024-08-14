using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Interfaces
{
    public interface IReadOnlyUserAddressRepository
    {
        Task<UserStreetAddress?> GetByIdAsync(Guid Id);
    }
}