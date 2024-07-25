using FastEndpoints;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.CartEndpoints;


public class ListCartResponse
{
    public List<CartItemDTO> CartItems { get; set; } = new();
}
internal class ListCartItems(IMediator mediator) : EndpointWithoutRequest<ListCartResponse>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get("/cart");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var email = User.FindFirstValue("EmailAddress");

        var query = new ListCartItemsQuery(email);

        var result = await _mediator.Send(query);

        if(result.Status == Ardalis.Result.ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync();
        } 
        else
        {
            var cartResponse = new ListCartResponse() { CartItems = result.Value };

            await SendAsync(cartResponse);
        }



    }
}
