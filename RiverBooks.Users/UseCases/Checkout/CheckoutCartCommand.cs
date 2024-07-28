using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.UseCases.Checkout
{
    public record CheckoutCartCommand(string EmailAddress,
                                        Guid ShippingAddressId,
                                        Guid BillingAddressId) : IRequest<Result<Guid>>;
}
