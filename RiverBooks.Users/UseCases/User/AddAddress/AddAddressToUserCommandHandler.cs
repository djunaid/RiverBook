using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.UseCases.User.AddAddress
{
    internal class AddAddressToUserCommandHandler : IRequestHandler<AddAddressToUserCommand, Result>
    {
        private readonly IApplicatinUserRepository _userRepository;
        private readonly ILogger _logger;
        public AddAddressToUserCommandHandler(IApplicatinUserRepository userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }


        public async Task<Result> Handle(AddAddressToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithAddressByEmailAsync(request.EmailAddress);

            if (user is null)
            {
                return Result.Unauthorized();
            }

            var addressToAdd = new Address(request.Street1, request.Street2, request.City, request.State, request.PostalCode, request.Country);


            var userAddress = user.AddAddress(addressToAdd);

            await _userRepository.SaveChangesAsync();
            _logger.Information("[UseCase] Added address {address} to user {email} (Total: {addressCount}",
                userAddress.StreetAddress, request.EmailAddress, user.UserAddresses.Count);

            return Result.Success();
        }
    }
}
