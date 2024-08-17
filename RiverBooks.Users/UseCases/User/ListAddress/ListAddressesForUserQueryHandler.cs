using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.UseCases.User.ListAddress
{
    public class ListAddressesForUserQueryHandler : IRequestHandler<ListAddressesForUserQuery, Result<List<UserStreetAddressDTO>>>
    {
        private readonly IApplicatinUserRepository _userRepository;


        public ListAddressesForUserQueryHandler(IApplicatinUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<List<UserStreetAddressDTO>>> Handle(ListAddressesForUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithAddressByEmailAsync(request.emailAddress);

            if (user is null)
            {
                return Result.Unauthorized();
            }

            var userAddresses = user.UserAddresses
                .Select(item =>
                new UserStreetAddressDTO(item.Id, item.UserId,
                                       item.StreetAddress.Street1,
                                       item.StreetAddress.Street2,
                                       item.StreetAddress.City,
                                       item.StreetAddress.State,
                                       item.StreetAddress.PostalCode,
                                       item.StreetAddress.Country))
                .ToList();

            return Result.Success(userAddresses);
        }
    }

}
