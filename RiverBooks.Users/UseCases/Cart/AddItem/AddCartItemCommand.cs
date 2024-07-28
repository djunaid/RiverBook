using MediatR;
using Ardalis.Result;

namespace RiverBooks.Users.UseCases.Cart.AddItem;

public record AddCartItemCommand(string EmailAddress, Guid BookId, int Quantity) : IRequest<Result>;
