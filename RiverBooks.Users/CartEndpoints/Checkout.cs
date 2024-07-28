using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases.Checkout;
using System.Security.Claims;

namespace RiverBooks.Users.CartEndpoints
{
    public record CheckoutRequest(Guid ShippingAddressId, Guid BillingAddressId);

    public record CheckoutResponse(Guid OrderId);
    internal class Checkout : Endpoint<CheckoutRequest, CheckoutResponse>
    {
        private readonly IMediator _mediator;

        public Checkout(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("/cart/checkout");
            Claims("EmailAddress");
        }

        public override async Task HandleAsync(CheckoutRequest req, CancellationToken ct)
        {
            var emailAddress = User.FindFirstValue("EmailAddress");

            var command = new CheckoutCartCommand(emailAddress,
                                                  req.ShippingAddressId,
                                                  req.BillingAddressId);

            var result = await _mediator.Send(command);

            if(result.Status == ResultStatus.Unauthorized)
            {
                await SendUnauthorizedAsync();
                return;
            }

            await SendOkAsync(new CheckoutResponse(result.Value));
            
        }

    }
}
