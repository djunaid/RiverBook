using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.UseCases.Checkout
{
    internal class CheckoutCartCommandHandler : IRequestHandler<CheckoutCartCommand, Result<Guid>>
    {
        private readonly IMediator _mediator;
        private readonly IApplicatinUserRepository _userRepository;

        public CheckoutCartCommandHandler(IMediator mediator, IApplicatinUserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(CheckoutCartCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

            if( user is null )
            {
                return Result.Unauthorized();
            }

            var orderItems = user.CartItems.Select(item =>
                               new OrderItemDetails (
                                   item.BookId,
                                   item.Quantity,
                                   item.UnitPrice,
                                   item.Description
                               )).ToList();

            var command = new CreateOrderCommand(Guid.Parse(user.Id), request.ShippingAddressId, request.BillingAddressId, orderItems);

            //TODO : Consider replacing with message-base approach for perf reasons

            var result = await _mediator.Send(command);

            if(!result.IsSuccess)
            {
                result.Map(x => x.OrderId);
            }

            user.ClearCart();
            await _userRepository.SaveChangesAsync();

            return Result.Success(result.Value.OrderId);
        }


    }
}
