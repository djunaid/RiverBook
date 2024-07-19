namespace RiverBooks.Users;

public interface IApplicatinUserRepository {

    Task<ApplicationUser> GetUserWithCartByEmailAsync(string emailAddress );
    Task SaveChangesAsync();
}
