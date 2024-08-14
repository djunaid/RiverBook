using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.UseCases.User
{
    internal record AddAddressToUserCommand (string EmailAddress, string Street1, string Street2, string City, string State, string PostalCode, string Country) : IRequest<Result>;
   
}
