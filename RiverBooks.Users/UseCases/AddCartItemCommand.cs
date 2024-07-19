using MediatR;
using Ardalis.Result;

namespace RiverBooks.Users;

public record AddCartItemCommand (string EmailAddress, Guid BookId, int Quantity) : IRequest<Result>;


public class AddCartItemHandler : IRequestHandler<AddCartItemCommand, Result>
{
    private readonly IApplicatinUserRepository _userRepository;

    public AddCartItemHandler(IApplicatinUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
         var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

         if (user == null){
            return Result.Unauthorized();
         }

         //TODO : Get Books detail

         var cart = new CartItem(Guid.NewGuid(),request.BookId,"description",request.Quantity,0.9m);

         await _userRepository.SaveChangesAsync();

         return Result.Success();
    }
}
