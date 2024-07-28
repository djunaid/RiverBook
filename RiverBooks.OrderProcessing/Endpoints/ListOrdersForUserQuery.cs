using Ardalis.Result;
using MediatR;

namespace RiverBooks.OrderProcessing.Endpoints
{
    internal record ListOrdersForUserQuery(string emailAddress) : IRequest<Result<List<OrderSummary>>>;
    
}
