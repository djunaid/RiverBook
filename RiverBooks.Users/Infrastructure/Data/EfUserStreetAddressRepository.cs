using Microsoft.EntityFrameworkCore;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.Infrastructure.Data
{
    internal class EfUserStreetAddressRepository : IReadOnlyUserAddressRepository
    {
        private readonly UserDBContext _dbContext;

        public EfUserStreetAddressRepository(UserDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<UserStreetAddress?> GetByIdAsync(Guid Id)
        {
            return _dbContext.UserStreetAddresses
                .SingleOrDefaultAsync(x => x.Id == Id);
        }
    }
}
