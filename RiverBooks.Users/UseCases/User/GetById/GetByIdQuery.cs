using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.UseCases.User.GetById
{
    internal record GetByIdQuery (Guid UserId) : IRequest<Result<UserDTO>>;
}
