using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Contracts;
using RiverBooks.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.Integrations
{
    internal class UserAddressDetailsByIdQueryHandler : IRequestHandler<UserAddressDetailsByIdQuery, Result<UserAddressDetails>>
    {
        private readonly IReadOnlyUserAddressRepository _readOnlyUserRepository;


        public UserAddressDetailsByIdQueryHandler(IReadOnlyUserAddressRepository readOnlyUserRepository)
        {
            _readOnlyUserRepository = readOnlyUserRepository;
        }

        public async Task<Result<UserAddressDetails>> Handle(UserAddressDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var address = await _readOnlyUserRepository.GetByIdAsync(request.AddressId);

            if (address == null)
            {
                return Result.NotFound();
            }

            var userAddress = new UserAddressDetails(address.UserId, address.Id, address.StreetAddress.Street1, address.StreetAddress.Street2, address.StreetAddress.City
                , address.StreetAddress.PostalCode, address.StreetAddress.State ,address.StreetAddress.Country);
            return Result.Success(userAddress);
        }
    }
}
