using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Interfaces;

public interface IApplicatinUserRepository
{

    Task<ApplicationUser> GetUserWithCartByEmailAsync(string email);

    Task<ApplicationUser> GetUserWithAddressByEmailAsync(string email);
    Task SaveChangesAsync();
}
