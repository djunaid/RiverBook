using MediatR;
using Ardalis.Result;
using RiverBooks.Books.Contracts;
using RiverBooks.Users.Interfaces;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.UseCases.Cart.AddItem;

public class AddCartItemHandler(IApplicatinUserRepository userRepository, IMediator mediator) : IRequestHandler<AddCartItemCommand, Result>
{
    private readonly IApplicatinUserRepository _userRepository = userRepository;
    private readonly IMediator _mediator = mediator;

    public async Task<Result> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

        if (user == null)
        {
            return Result.Unauthorized();
        }


        var query = new BookDetailsQuery(request.BookId);
        var result = await _mediator.Send(query);

        if (result.Status == ResultStatus.NotFound)
        {
            return Result.NotFound();
        }

        var description = $"{result.Value.Title} by {result.Value.Author}";

        var cart = new CartItem(request.BookId, description, request.Quantity, result.Value.Price);

        user.AddItemToCart(cart);
        await _userRepository.SaveChangesAsync();

        return Result.Success();
    }
}
