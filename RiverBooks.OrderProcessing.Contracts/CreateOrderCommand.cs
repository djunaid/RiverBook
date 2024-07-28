using MediatR;
using Ardalis.Result;

namespace RiverBooks.OrderProcessing.Contracts
{
    public record CreateOrderCommand(Guid UserId,
                                     Guid ShippingAddressId,
                                     Guid BillingAddressId,
                                     List<OrderItemDetails> OrderItems) : IRequest<Result<OrderDetailResponse>>;
    
}
