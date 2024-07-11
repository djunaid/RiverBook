using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users;

public record LoginRequest(string Email, string Password);
public class LoginEndpoint : Endpoint<LoginRequest>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public LoginEndpoint(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public override void Configure()
    {
        Post("/users/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
       var user = await _userManager.FindByEmailAsync(req.Email);

       if(user is null){
        await SendUnauthorizedAsync();
        return;
       }

       var loginSuccessful = await _userManager.CheckPasswordAsync(user, req.Password);

       if(!loginSuccessful){
        await SendUnauthorizedAsync();
        return;
       }

       var jwtSecret = Config["Auth:JwtSecret"];
       var token = JWTBearer.CreateToken(jwtSecret, claims: new[] {("EmailAddress", req.Email) });
       await SendAsync(token);

    }

}
