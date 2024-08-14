using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using RiverBooks.Users.Domain;
using Serilog;

namespace RiverBooks.Users;

public record CreateRequest (string Email, string Password);
internal class Create : Endpoint<CreateRequest>
{

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly ILogger _logger;

    public Create (UserManager<ApplicationUser> userManager,
        ILogger logger){
        _userManager = userManager;
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateRequest req, CancellationToken ct)
    {
        var newUser = new ApplicationUser{ 
            Email = req.Email,
            UserName = req.Email
        };

        _logger.Information("{userEmail} email and {password} password is set to be created"
        ,req.Email,req.Password);

        var result = await _userManager.CreateAsync(newUser, req.Password);

        _logger.Information($"user created {result.Succeeded}");

        await SendOkAsync();
    }

}
