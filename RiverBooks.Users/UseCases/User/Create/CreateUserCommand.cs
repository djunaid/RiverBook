using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.UseCases.User.Create;

internal record CreateUserCommand(string Email, string Password) : IRequest<Result>;
