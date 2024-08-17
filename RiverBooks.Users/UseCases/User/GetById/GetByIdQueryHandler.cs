using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.UseCases.User.GetById
{
    internal class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<UserDTO>>
    {
        private readonly IApplicatinUserRepository _userRepository;

        public GetByIdQueryHandler(IApplicatinUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDTO>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserEmailById(request.UserId.ToString());

            if(user is null)
            {
                return Result.NotFound();
            }

            return Result.Success(new UserDTO(user.Id, user.Email));
        }
    }
}
