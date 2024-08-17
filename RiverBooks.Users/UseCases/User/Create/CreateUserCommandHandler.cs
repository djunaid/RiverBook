using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RiverBooks.EmailSending;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.UseCases.User.Create;

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediator _mediator;

    public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, UserManager<ApplicationUser> userManager, IMediator mediator)
    {
        _logger = logger;
        _userManager = userManager;
        _mediator = mediator;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email
        };

        _logger.LogInformation("{userEmail} email is set to be created"
        , request.Email);

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (!result.Succeeded)
        {
            return Result.Error(result.Errors.SingleOrDefault().Description);
        }

        _logger.LogInformation($"user created {result.Succeeded}");

        var sendEmailCommand = new SendEmailCommand { Body = $"Welcome to RiverBook {request.Email}", To = request.Email, From = "donotreply@riverbook.com", Subject = "You have signed up for RiverBook" };

        _ = await _mediator.Send(sendEmailCommand);

        return Result.Success();
        
    }
}
