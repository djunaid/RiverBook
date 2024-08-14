using Microsoft.EntityFrameworkCore;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.Infrastructure.Data;

internal class EfApplicationUserRepository : IApplicatinUserRepository
{
    private readonly UserDBContext _dbContext;

    public EfApplicationUserRepository(UserDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<ApplicationUser> GetUserWithAddressByEmailAsync(string email)
    {
        var applicationUsers = _dbContext.ApplicationUsers.Include(user => user.UserAddresses);
        var result = applicationUsers.SingleAsync(x => x.Email == email);

        return result;
        // return _dbContext.ApplicationUsers.Include(user=> user.Addresses)
        //                 .SingleAsync(user=> user.Email == email);
    }

    public Task<ApplicationUser> GetUserWithCartByEmailAsync(string email)
    {
        return _dbContext.ApplicationUsers
          .Include(user => user.CartItems)
          .SingleAsync(user => user.Email == email);
    }

    public Task SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}
