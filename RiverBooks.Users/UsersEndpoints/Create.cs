using Ardalis.Result.AspNetCore;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RiverBooks.Users.Domain;
using RiverBooks.Users.UseCases.User.Create;
using Serilog;

namespace RiverBooks.Users;

public record CreateRequest (string Email, string Password);
internal class Create : Endpoint<CreateRequest>
{

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly ILogger _logger;

    private readonly IMediator _mediator;

    public Create(UserManager<ApplicationUser> userManager,
        ILogger logger,
        IMediator mediator)
    {
        _userManager = userManager;
        _logger = logger;
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateRequest req, CancellationToken ct)
    {
        var command = new CreateUserCommand(req.Email, req.Password);
        
        var result = await _mediator.Send(command);

        if(result.IsSuccess)
        {
            await SendOkAsync();
        } 
        else
        {
            await SendResultAsync(result.ToMinimalApiResult());
            return;
        }

        
    }

}
