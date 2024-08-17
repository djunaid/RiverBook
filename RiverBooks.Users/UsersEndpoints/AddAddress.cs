using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases.User;
using RiverBooks.Users.UseCases.User.AddAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.UsersEndpoints
{
    internal record AddAddressRequest(string Street1, string Street2, string City, string State, string PostalCode, string Country);
    internal class AddAddress : Endpoint<AddAddressRequest>
    {
        private readonly IMediator _mediator;

        public AddAddress(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("/user/address");
            Claims("EmailAddress");
        }

        public override async Task HandleAsync(AddAddressRequest req, CancellationToken ct)
        {
            var emailAddress = User.FindFirstValue("EmailAddress");

            var command = new AddAddressToUserCommand(emailAddress,
                                                      req.Street1,
                                                      req.Street2,
                                                      req.City,
                                                      req.State,
                                                      req.PostalCode,
                                                      req.Country);

            var result = await _mediator.Send(command);

            if(result.Status == ResultStatus.Unauthorized)
            {
                await SendUnauthorizedAsync();
            } 
            else
            {
               await SendOkAsync();
            }           
        }
    }
}
