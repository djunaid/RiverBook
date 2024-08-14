using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.Contracts
{
    public record UserAddressDetailsByIdQuery (Guid AddressId) : IRequest<Result<UserAddressDetails>>;
    
}
