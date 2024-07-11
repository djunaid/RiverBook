﻿using System.Security.Claims;
using FastEndpoints;
using MediatR;

namespace RiverBooks.Users;
public class AddItem : Endpoint<AddCartItemRequest>
{
    private readonly IMediator _mediator;

    public AddItem(IMediator mediator){
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/cart");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(AddCartItemRequest req, CancellationToken ct)
    {
        var emailAddress  = User.FindFirstValue("EmailAddress");

        var command = new AddCartItemCommand (  emailAddress, req.BookId , req.Quantity);
        var result = await _mediator.Send(command, ct);

        await SendOkAsync();
    }

}