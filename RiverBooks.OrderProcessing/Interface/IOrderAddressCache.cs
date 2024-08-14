using Ardalis.Result;
using RiverBooks.OrderProcessing.Infrastructure;

namespace RiverBooks.OrderProcessing.Interface
{
    internal interface IOrderAddressCache
    {
        Task<Result<OrderAddress>> GetByIdAsync(Guid addressId);
        Task<Result> StoreAsync(OrderAddress orderAddress);
    }
}