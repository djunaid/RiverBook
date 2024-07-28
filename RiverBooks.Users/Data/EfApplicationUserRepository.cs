using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users;

internal class EfApplicationUserRepository : IApplicatinUserRepository
{
    private readonly UserDBContext _dbContext;

    public EfApplicationUserRepository(UserDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<ApplicationUser> GetUserWithAddressByEmailAsync(string emailAddress)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationUser> GetUserWithCartByEmailAsync(string email)
    {
        return _dbContext.ApplicationUsers
          .Include(user=> user.CartItems)
          .SingleAsync(user => user.Email == email);
    }

    public Task SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}
