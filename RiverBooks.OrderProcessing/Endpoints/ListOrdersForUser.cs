using Ardalis.Result;
using FastEndpoints;
using MediatR;
using System.Security.Claims;

namespace RiverBooks.OrderProcessing.Endpoints
{
    internal class ListOrdersForUser(IMediator mediator) : EndpointWithoutRequest<ListOrdersForUserResponse>
    {
        private readonly IMediator _mediator = mediator;

        public override void Configure()
        {
            Get("/orders");
            Claims("EmailAddress");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var emailAddress = User.FindFirstValue("EmailAddress");

            var query = new ListOrdersForUserQuery(emailAddress);

            var result = await _mediator.Send(query);

            if (result.Status == ResultStatus.Unauthorized)
            {
                await SendUnauthorizedAsync();
                return;
            }
             
            var response = new ListOrdersForUserResponse();

            response.Orders = result.Value.Select(o =>
                new OrderSummary
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    CreatedDate = o.CreatedDate,
                    ShippedDate = o.ShippedDate,
                    Total = o.Total
                }
            ).ToList();

            await SendAsync(response);
        }
    }

}
