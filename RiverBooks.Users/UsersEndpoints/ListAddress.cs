using FastEndpoints;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using RiverBooks.Users.UseCases.User;

namespace RiverBooks.Users.UsersEndpoints
{
    public class ListAddressResponse
    {
        public List<UserStreetAddressDTO> Addresses { get; set; } = new();
    }
    internal class ListAddress : EndpointWithoutRequest<ListAddressResponse>
    {
        private readonly IMediator _mediator;

        public ListAddress(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("user/addresses");
            Claims("EmailAddress");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var emailAddress = User.FindFirstValue("EmailAddress");

            var query = new ListAddressesForUserQuery(emailAddress);

            var result = await _mediator.Send(query);

            if(result.Status == ResultStatus.Unauthorized)
            {
                await SendUnauthorizedAsync();
            }
            else
            {
                
                await SendAsync(new ListAddressResponse { Addresses = result.Value });
            }
        }
    }
    
}
