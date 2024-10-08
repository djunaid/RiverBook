﻿

using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users;

public class ListUsers {

    public List<ApplicationUser> Users { get; set; }
}

internal class List(UserManager<ApplicationUser> userManager) : EndpointWithoutRequest<ListUsers>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public override void Configure()
    {
        Get("/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var users = await _userManager.Users.ToListAsync();
        
        await SendAsync(new ListUsers{ Users = users});
    }
}
