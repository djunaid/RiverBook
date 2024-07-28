using Ardalis.Result;
using MediatR;
using RiverBooks.Users.CartEndpoints;

namespace RiverBooks.Users.UseCases.Cart.ListItem
{
    public record ListCartItemsQuery(string emailAddress) : IRequest<Result<List<CartItemDTO>>>;

    internal class ListCartItemsHandler : IRequestHandler<ListCartItemsQuery, Result<List<CartItemDTO>>>
    {
        private readonly IApplicatinUserRepository _userRepository;

        public ListCartItemsHandler(IApplicatinUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<List<CartItemDTO>>> Handle(ListCartItemsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithCartByEmailAsync(request.emailAddress);

            if (user is null)
            {
                return Result.Unauthorized();
            }

            return user.CartItems
                .Select(item => new CartItemDTO(item.Id, item.BookId, item.Description, item.Quantity, item.UnitPrice))
                .ToList();
        }
    }
}