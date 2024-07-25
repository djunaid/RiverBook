using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Books.Contracts
{

    public record BookDetailsQuery(Guid BookId) : IRequest<Result<BookDetailsResponse>>;
}
