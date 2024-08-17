using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.UseCases.User.ListAddress
{
    public record ListAddressesForUserQuery(string emailAddress) : IRequest<Result<List<UserStreetAddressDTO>>>;

}
